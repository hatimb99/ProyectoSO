#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>
#include <pthread.h>
#include<stdbool.h>

typedef struct {
	char nombre [20];
	int socket;
} Conectado;

typedef struct {
	int socket;
	int puntos;
	//int posListaConectados;
	int abandonoPartida;
} Jugador;


typedef struct {
	int idPartida;
	Jugador jugadores [4];
	int numJugadores;
	int jugadorEnTurno;
	int finalizada;
} Partida;

typedef struct {
	Conectado conectados [100];
	int num;
} ListaConectados;

typedef struct {
	Partida partidas [20];
	int num;
} ListaPartidas;

typedef enum {
DESCONEXION_EXITOSA			= 0,
	CONSULTA_NRO_VICTORIAS		= 1,
	CONSULTA_MAS_PUNTOS		= 2,
	CONSULTA_GANADOR_PARTIDA	= 3,
	LOGIN				= 4,
	REGISTRO			= 5,
	CAMBIO_LISTA_JUGADORES		= 6,
	INVITAR_PARTIDA		= 7,
	ACEPTA_JUEGO			= 8,
	INICIO_PARTIDA			= 9,
	JUGADA_HECHA			= 10,
	FIN_PARTIDA			= 11,
	MENSAJE_CHAT			= 12,
	RESULTADO_CONEXION		= 13
} EPeticiones;

char conectados[500];
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;

ListaConectados lista;
ListaPartidas listaPartidas;
int ultIdPartida = 0;

void PonConectado (ListaConectados *lista, char nombre[20], int socket) 
{
	strcpy (lista->conectados[lista->num].nombre, nombre);
	lista->conectados[lista->num].socket = socket;
	lista->num = lista->num + 1;
}

int BuscarConectado(ListaConectados *lista, int socket)
{
	int i;
	for (i=0; i< lista->num; i++)
	{
		if (lista->conectados[i].socket == socket)
			return i;	
	}
	return -1;
}

int BuscarPartida(ListaPartidas *lista, int idPartida)
{
	int i;
	for (i=0; i< lista->num; i++)
	{
		if (lista->partidas[i].idPartida == idPartida)
			return i;
		return -1;
	}
}

int ActualizaConectado (ListaConectados *lista, char nombre[20], int socket) 
{
	int pos = BuscarConectado(lista,socket);
	if (pos != -1)
	{
		strcpy (lista->conectados[pos].nombre, nombre);
	}
	return pos;
}

void DameConectados (ListaConectados *lista, char conectados[500]) {
	// Pone en conectados el nombre y socket de cada conectado separado por ; 
	// La informacion de cada conectado separada por / 
	// Primero pone el numero de conectados. Ejemplo: "3/Juan;12/Maria;24/Pedro;15"
	
	int i;
	int sesiones = 0;
	// contar las saseiones iniciadas
	for (i=0; i < lista->num; i++)
	{

		if (strcmp(lista->conectados[i].nombre,"") != 0)
			sesiones++;
	}
	sprintf (conectados, "%d", sesiones);
	for (i=0; i < lista->num; i++)
	{
		if (strcmp(lista->conectados[i].nombre,"") != 0)
			sprintf (conectados, "%s/%s;%d", conectados, lista->conectados[i].nombre,lista->conectados[i].socket);
	}
	printf("Conectados %s\n", conectados);
}

void EliminarConectado(ListaConectados *lista,int socket) {
	int i=0, eliminado = 0,j;
	while (eliminado == 0) {
		if (lista->conectados[i].socket == socket) {
			for (j=i;j<lista->num-1;j++) {
				lista->conectados[j] = lista->conectados[j+1];
			}
			lista->num--;
			//bool= true;
		}
		else
			i++;
	}
}


void EnviarRespuesta(char respuesta[],int sock_conn)
{
	printf ("Respuesta: %s\n", respuesta);
	// Enviamos respuesta
	write (sock_conn,respuesta, strlen(respuesta));			
}

void Login(MYSQL *conn, char nombre[], char contrasena[], int sock_conn,ListaConectados *lista)
{
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	char respuesta[512];
	strcpy(consulta, "SELECT JUGADOR.USERNAME FROM (JUGADOR)"
		   " WHERE JUGADOR.USERNAME = '");
	strcat (consulta, nombre);
	strcat (consulta, "'");
	printf("%s\n",consulta);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);

	if (row == NULL)
		sprintf (respuesta,  "%i/NO_OK/Usuario no registrado\n",(int)LOGIN);

	else
	{
		MYSQL_RES *resultado2;
		strcpy(consulta, "SELECT JUGADOR.CONT FROM (JUGADOR)"
			   " WHERE JUGADOR.CONT = '");
		strcat (consulta, contrasena);
		strcat (consulta, "' AND JUGADOR.USERNAME='");
		strcat (consulta, nombre);
		strcat (consulta, "'");
		
		err=mysql_query (conn, consulta);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		resultado2 = mysql_store_result (conn);
		row = mysql_fetch_row (resultado2);
		
		if (row == NULL)
			sprintf (respuesta, "%i/NO_OK/Contrasenha incorrecta",(int)LOGIN);
	
		else 
			if (strcmp(row[0],contrasena)==0)
			{	
				int pos;
				pthread_mutex_lock( &mutex );//No me interrumpas ahora
				printf("voy a buscar a %s con sock %d\n", nombre, sock_conn);
				printf("Hay dos %d sesiones\n", lista->num);
				pos = ActualizaConectado(lista, nombre, sock_conn);
				printf("lo encontre en %d\n", pos);
				pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
				if (err == -1)
				{
					// error desconocido en el login
				}
				else
				{
					// enviar al usuario respuesta de login exitoso y lista de los conectados
					DameConectados (lista, conectados);
					sprintf (respuesta, "%d/OK/%s",(int)LOGIN,conectados);
					printf("%s\n", respuesta);
					//notificar a todos los clientes conectados la nueva lista
					char notificacion[600];
					sprintf (notificacion, "%d/OK/%s",(int)CAMBIO_LISTA_JUGADORES,conectados);
					printf("%s\n", notificacion);
					int j;
					for (j=0; j< lista->num; j++)
					{
						//if (j != pos)
						printf("RESPUESTA PARA EL SOCK %d\n",lista->conectados[j].socket);
							write (lista->conectados[j].socket, notificacion, strlen(notificacion));
					}
				}
			}
			else 
				sprintf (respuesta, "%i/NO_OK/Contrasenha incorrecta",(int)LOGIN);
	}
	printf("RESPUESTA PARA EL SOCK %d\n",sock_conn);
	EnviarRespuesta(respuesta,sock_conn);					
}

void Registrar(MYSQL *conn,char nombre[], char contrasena[], int sock_conn)
{
	char consulta[200];
	int IDJ, err;
	char idj[80];
	char respuesta[512];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta, "SELECT MAX(JUGADOR.IDJ) FROM (JUGADOR)");

	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);

	row = mysql_fetch_row (resultado);

	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
		IDJ = atoi (row[0]);
	IDJ=IDJ+1;
	sprintf(idj, "%d", IDJ);
	MYSQL_RES *resultado5;
	strcpy(consulta, "SELECT JUGADOR.USERNAME FROM (JUGADOR) WHERE JUGADOR.USERNAME = '");
	strcat(consulta,nombre);
	strcat(consulta, "';");

	printf("%s\n",consulta);

	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado5 = mysql_store_result (conn);
	row = mysql_fetch_row (resultado5);
	if (row == NULL)
	{
		char insertar[200];
		strcpy(insertar, "INSERT INTO JUGADOR VALUES (");
		strcat(insertar, idj);
		strcat(insertar, ",'");
		strcat(insertar, nombre);
		strcat(insertar, "','");
		strcat(insertar, contrasena);
		strcat(insertar,"');");
		
		err=mysql_query (conn, insertar);
		if (err!=0) {
			printf ("Error al insertar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		sprintf(respuesta, "%i/Usuario registrado, por favor inicie sesion si quiere hacer una consulta.",(int)REGISTRO);
	}
	else
		sprintf(respuesta, "%i/Usuario ya existe, por favor inicie sesión",(int)REGISTRO);
	
	EnviarRespuesta(respuesta,sock_conn);
}

void NumeroVictorias(MYSQL *conn, char nombre[], int sock_conn)
{
	char consulta [300];
	strcpy (consulta,"SELECT COALESCE(COUNT(GANADOR),0) AS num_victorias FROM PARTIDA WHERE PARTIDA.GANADOR ='");
	strcat (consulta, nombre);
	strcat (consulta,"';");
	char respuesta[512];
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	int i=0;
	if (row == NULL)
	{
		printf ("No se han obtenido datos en la consulta\n");
		sprintf (respuesta, "%d/No se han obtenido datos en la consulta\n",(int)CONSULTA_NRO_VICTORIAS);
	}
	else
	{
		sprintf (respuesta, "%d/Ha ganado %i veces\n", (int)CONSULTA_NRO_VICTORIAS,atoi(row[0]));
		
	}
	EnviarRespuesta(respuesta,sock_conn);

}

void JugadorMasPuntos(MYSQL *conn, int sock_conn)
{
	int puntos,err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta [300];
	char respuesta[512];

	strcpy (consulta, "SELECT JUGADOR.USERNAME, COALESCE(SUM(PUNTUACION.PUNTOS),0) AS puntos FROM (JUGADOR,PARTIDA,PUNTUACION)"
       			" WHERE (PUNTUACION.ID_J=JUGADOR.IDJ) AND (PUNTUACION.ID_P=PARTIDA.ID) GROUP BY JUGADOR.USERNAME ORDER BY puntos DESC LIMIT 1");
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}

	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);

	if (row== NULL) {
		printf("No se han obtenido datos en la consulta\n");
		sprintf(respuesta, "%d/No hay datos\n",(int)CONSULTA_MAS_PUNTOS);
	}
	else {
		puntos = atoi (row[1]);
		sprintf(respuesta, "%d/%s es quien mas puntos tiene con %d puntos\n", (int)CONSULTA_MAS_PUNTOS,row[0],puntos);
	}
	EnviarRespuesta(respuesta,sock_conn);	
}

void GanadorPartida(MYSQL *conn, int sock_conn,int idPartida)
{
	MYSQL_RES *resultado9;
	MYSQL_ROW row9;
	int IDJ;
	int id,err;
	char ganador [20];
	char consulta[100];
	char respuesta[512];
	char nombre[20]="";
	char partida[4];
	sprintf (partida, "%d",idPartida);
	strcpy (consulta,"SELECT PARTIDA.GANADOR, JUGADOR.IDJ FROM PARTIDA,JUGADOR WHERE PARTIDA.ID = ");
	strcat (consulta, partida);
	strcat (consulta," AND PARTIDA.GANADOR = JUGADOR.USERNAME;");
	
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	

	//recogemos el resultado de la consulta
	resultado9 = mysql_store_result (conn);

	row9 = mysql_fetch_row (resultado9);
	if (row9 == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
		id = atoi (row9[1]);
	//la columna 0 contiene tiene el nombre del ganador
	//la columna 1 contiene tiene el ID del ganador
	sprintf (respuesta,"3/Nombre del ganador: %s, ID: %d\n", row9[0],id);

	EnviarRespuesta(respuesta,sock_conn);
}

void Invitar_partida(MYSQL *conn, int sock_conn, int sockInvitado)
{
	char notificacion[500];
	sprintf (notificacion, "%d/%d/Quieres unirte a mi partida?",(int)INVITAR_PARTIDA,sock_conn);
	printf("%s\n", notificacion);
	int j;
	write (sockInvitado, notificacion, strlen(notificacion));
}

/* ***************** Funcion de atención de peticiones del cliente ******************** */
void *AtenderCliente (void *socket)
{
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexin
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "bd" ,0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	int encontrado=0;
	
	int sock_conn = *((int*)socket);
	
	int ret;
	char peticion[512];
	char respuesta[512];

	int terminar =0;
	// Agregar el socket a los conectados 
	pthread_mutex_lock( &mutex );//No me interrumpas ahora
		PonConectado(&lista, "", sock_conn);
	pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
	// enviar como respuesta el socket asignado
	sprintf (respuesta, "13/OK;%i\n", sock_conn);
	EnviarRespuesta(respuesta,sock_conn);

	// Entramos en un bucle para atender todas las peticiones de este cliente
	// hasta que se desconecte
	while (terminar ==0)
	{
		// Se recibe la peticion
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int accion =  atoi (p);
		// Ya tenemos el codigo de la peticion
		char nombre[20]="";
		char contrasena[80]="";

		switch ((EPeticiones)accion)
		{
			case DESCONEXION_EXITOSA:
				printf ("Cerrando la Conexion\n");
				terminar=1;
				// eliminar de la lista de conectados
				EliminarConectado(&lista,sock_conn);
				//si tiene partidas en juego poner que abandona
				break;
			case  CONSULTA_NRO_VICTORIAS:
				p = strtok( NULL, "/");
				if (p)
				{
					strcpy (nombre, p);
					NumeroVictorias(conn, nombre,sock_conn);
				}
				break;
			case CONSULTA_MAS_PUNTOS:
				JugadorMasPuntos(conn, sock_conn);
				break;
			case CONSULTA_GANADOR_PARTIDA:
				p = strtok( NULL, "/");
				if (p)
				{
					int idPartida =  atoi (p);
					GanadorPartida(conn, sock_conn,idPartida);
					break;
				}
			case LOGIN :
				p = strtok( NULL, "/");
				if (p)
				{
					strcpy (nombre, p);
					p = strtok( NULL, "/");
					if (p)
					{
						strcpy (contrasena, p);
						// Ya tenemos el nombre
						printf ("Codigo: %d, Nombre: %s, Contraseña: %s\n", accion, nombre, contrasena);
					}
				}
				Login(conn, nombre, contrasena, sock_conn,&lista);				
				break;	
			case REGISTRO :
				p = strtok( NULL, "/");
				if (p)
				{
					strcpy (nombre, p);
					p = strtok( NULL, "/");
					if (p)
					{
						strcpy (contrasena, p);
						// Ya tenemos el nombre
						printf ("Codigo: %d, Nombre: %s, Contraseña: %s\n", accion, nombre, contrasena);
						Registrar(conn, nombre, contrasena, sock_conn);
					}
				}
								
				break;	
			case CAMBIO_LISTA_JUGADORES:
				break;	
			case INVITAR_PARTIDA:
				{
					p = strtok( NULL, "/");
					if (p)
					{
						int numInvitados =  atoi (p);
						int i;
						p = strtok( NULL, "/");
						for(i=0; i< numInvitados;i++)
						{
							if (p)
							{
								int socketInvitado =  atoi (p);
								Invitar_partida(conn,sock_conn,socketInvitado);
								p = strtok( NULL, "/");
							}
						}
					}
				}
				break;
			case ACEPTA_JUEGO:
				{
					char acepta[5];
					p = strtok( NULL, "/");
					if (p)
					{
						strcpy (acepta, p);
						p = strtok( NULL, "/");
						if (p)
						{
							int socketAnfitrion =  atoi(p);
							// Se envia la respuesta al jugador que esta invitando
							
							sprintf (respuesta, "%d/%s/%d",(int)ACEPTA_JUEGO,acepta,sock_conn);
							printf("%s\n", respuesta);
							write (socketAnfitrion, respuesta, strlen(respuesta));
						}	
					}
				}
				break;	
			case INICIO_PARTIDA:
				{
					int i;
					p = strtok( NULL, "/");
					if (p)
					{
						int numJugPartida =  atoi(p);
						// se agrega el anfitrion a la lista de jugadores
						numJugPartida++;
						char datosJugadores[100];
						pthread_mutex_lock( &mutex);
						// agrega la partida a la lista de partidas
						ultIdPartida++;
						listaPartidas.partidas[listaPartidas.num].idPartida = ultIdPartida;
						listaPartidas.partidas[listaPartidas.num].jugadorEnTurno = 0;
						listaPartidas.partidas[listaPartidas.num].finalizada = 0;
						listaPartidas.partidas[listaPartidas.num].numJugadores = numJugPartida;						
						
						listaPartidas.partidas[listaPartidas.num].jugadores[0].socket =sock_conn;
						listaPartidas.partidas[listaPartidas.num].jugadores[i].puntos = 0;
						listaPartidas.partidas[listaPartidas.num].jugadores[i].abandonoPartida = 0;	
						sprintf (datosJugadores, "/%d",sock_conn);
						// agrega el resto de jugadores					
						for(i=1; i<=numJugPartida;i++)
						{
							p = strtok( NULL, "/");
							if (p)
							{
								int socketInvitado = atoi(p);
								listaPartidas.partidas[listaPartidas.num].jugadores[i].socket =socketInvitado;
								listaPartidas.partidas[listaPartidas.num].jugadores[i].puntos = 0;
								listaPartidas.partidas[listaPartidas.num].jugadores[i].abandonoPartida = 0;
								// de una vez se arma el string con los lista de jugadores
								char cadena[10];							
								sprintf (cadena, "/%d",socketInvitado);
								strcat (datosJugadores, cadena);
							}
						}
						listaPartidas.num++;
						pthread_mutex_unlock( &mutex);
						// envia a cada jugador informacion del id de la partida,el socket de cada jugador que participa, y el primer turno
						// al anfitrion le corresponde el primer turno
						// Formato: 9/J/IdPArtida/turno/numJugadores/Socket cada jugador
						sprintf (respuesta, "%d/%s/%d/%d/0",(int)INICIO_PARTIDA,"J",ultIdPartida,numJugPartida);
						strcat (respuesta, datosJugadores);
						for(i=1; i<=numJugPartida;i++)
						{
							write (listaPartidas.partidas[listaPartidas.num-1].jugadores[i].socket, respuesta, strlen(respuesta));
						}
						// envia al anfitrion el Id de la partida
						// Formato: 9/A/IdPartida/turno							
						sprintf (respuesta, "%d/%s/%d/0",(int)INICIO_PARTIDA,"A",ultIdPartida);
						printf("%s\n", respuesta);
						write (sock_conn, respuesta, strlen(respuesta));
					
					}
				}
				break;
			case JUGADA_HECHA:
				// incrementar el turno
				p = strtok (NULL, "/");
				if (p)
				{
					int idPartida =  atoi (p);
					p = strtok( NULL, "/");
					int misocket = atoi (p);
					p = strtok( NULL, "/");
					int puntos = atoi(p);
					
					char notificacion[600];
					sprintf (notificacion, "%d/%d/%d/%d",(int)JUGADA_HECHA,idPartida, misocket, puntos);
					int posArray = BuscarPartida(&listaPartidas, idPartida);
					printf("46785645465754\n");
					for (int j=0; j < listaPartidas.partidas[posArray].numJugadores ; j++){
						if (listaPartidas.partidas[posArray].jugadores[j].socket != misocket){
							write (listaPartidas.partidas[posArray].jugadores[j].socket, notificacion, strlen(notificacion));
							printf("999999999999999999\n");
						}
						
					}
				}
				// enviar el socket del que jugo la fila y los puntos
				break;
			case FIN_PARTIDA:
				// se recibe la ultima jugada y los puntos y hay que registrarlo en la base de datos
				break;
			case MENSAJE_CHAT:
				p = strtok( NULL, "/");
				if (p)
				{
					int idPartida =  atoi (p);
					p = strtok( NULL, "/");
					if (p)
					{
						char mensaje[200];
						int j;
						strcpy (mensaje, p);
						int posArray = BuscarPartida(&listaPartidas, idPartida);
						if (posArray != -1)
						{
							sprintf (respuesta, "%d/%d/%s",(int)MENSAJE_CHAT,idPartida,mensaje);
							printf("%s\n", respuesta);
							for (j=0; j < listaPartidas.partidas[posArray].numJugadores ; j++)
								write (listaPartidas.partidas[posArray].jugadores[j].socket, mensaje, strlen(mensaje));
						}		
					}	
				}	
				break;
			default:
				break;										
		}


	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	mysql_close (conn);
	printf ("Conexion Cerrada\n");
}

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
	
	lista.num=0;
	listaPartidas.num=0;
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
		
	// Fem el bind al port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9030);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	pthread_t thread;
	// Bucle para atender a 5 clientes
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacerprintf ("Escuchando\n");
		
		pthread_create (&thread, NULL, AtenderCliente,&sock_conn);
	}
}
