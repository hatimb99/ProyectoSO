#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>

typedef struct {
	char nombre [20];
	int socket;
} Conectado;

typedef struct {
	Conectado conectados [100];
	int num;
} ListaConectados;

typedef struct {
	char jugador [20];
	int socket;
	char estado [20];
} Jugadores;

typedef struct {
	char host [20];
	Jugadores jugadores [100];
	int num;
} ListaPartida;

/*typdef struct {*/
/*	ListaPartida partida [100];*/
/*	int num;*/
/*} ListaTotalPartidas;*/

char conectados[500];
char jugadores[500];
char estados[500];
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];

ListaConectados lista;
ListaPartida partida;

void Pon (ListaConectados *lista, char nombre[20], int socket) {
		strcpy (lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num = lista->num + 1;
}

void DameConectados (ListaConectados *lista, char conectados[300]) {
	// Pone en conectados los nombres de todos los conectados separados
	// por /. Primero pone le nÃºmero de conectados. Ejemplo:
	// "3/Juan/Maria/Pedro"
	sprintf (conectados, "%d", lista->num);
	int i;
	for (i=0; i < lista->num; i++)
		sprintf (conectados, "%s,%s", conectados, lista->conectados[i].nombre);
	printf("%s\n", conectados);
}

void crearPartida (ListaPartida *partida, char host[20], int socket, char jugador[20]) {
	strcpy (partida->jugadores[0].jugador, host);
	strcpy (partida->jugadores[partida->num+1].jugador, jugador);
	//strcpy (partida->jugadores[lista->num].estado, estado);
	partida->jugadores[partida->num].socket = socket;
	strcpy (partida->host,host);
	partida->num = partida->num + 1;

void estadojugadores(ListaPartida *partida, char estado[20]) {
	strcpy (partida->jugadores[partida->num].estado, estado);
}



void DameJugadoresyEstado (ListaPartida *partida, char jugadores[500]){
	sprintf (jugadores, "%d", partida->num);
	int i;
	for (i=0; i < partida->num; i++)
		sprintf (jugadores, "%s,%s-%s", jugadores, partida->jugadores[i].jugador, partida->jugadores[i].estado);
}

void *AtenderCliente (void *socket)
{
	//ListaConectados lista;
	//lista.num=0;
	//Inicializar bbdd
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
	
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	int ret;
	char peticion[512];
	char respuesta[512];

	int terminar =0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar ==0)
	{
		// Ahora recibimos la petici?n
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		// Ya tenemos el c?digo de la petici?n
		char nombre[20];
		char contrasena[80];
		if (codigo !=0 && codigo !=8)
		{
			p = strtok( NULL, "/");
			
			strcpy (nombre, p);
			p = strtok( NULL, "/");
			
			strcpy (contrasena, p);
			// Ya tenemos el nombre
			printf ("Codigo: %d, Nombre: %s, Contraseña: %s\n", codigo, nombre, contrasena);
		}
		char host[20];
		char invitado[20];
		if (codigo==8)
		{
			p = strtok( NULL, "/");
			
			strcpy (host, p);
			p = strtok( NULL, "/");
			
			strcpy (invitado, p);
		}
		
		if (codigo ==0) //petici?n de desconexi?n
			terminar=1;
		else if (codigo ==1 && encontrado !=0) 
		{
			
			char consulta [80];
			strcpy (consulta,"SELECT PARTIDA.GANADOR FROM PARTIDA WHERE PARTIDA.GANADOR ='");
			strcat (consulta, nombre);
			strcat (consulta,"';");
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta. El resultado de la
			//consulta se devuelve en una variable del tipo puntero a
			//MYSQL_RES tal y como hemos declarado anteriormente.
			//Se trata de una tabla virtual en memoria que es la copia
			//de la tabla real en disco.
			resultado = mysql_store_result (conn);
			// El resultado es una estructura matricial en memoria
			// en la que cada fila contiene los datos de una persona.
			
			// Ahora obtenemos la primera fila que se almacena en una
			// variable de tipo MYSQL_ROW
			row = mysql_fetch_row (resultado);
			int i=0;
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{
				while (row !=NULL) {
					i=i+1;
					row = mysql_fetch_row (resultado);
				}
				sprintf (respuesta, "1/Ha ganado %i veces\n", i);
				
			}
			
		}
		//2nda consulta: ¿Quien tiene mas puntos?
		else if (codigo ==2 && encontrado==1)
		{
			int puntos1;
			int puntos2;
			MYSQL_RES *resultado1;
			MYSQL_RES *resultado2;
			MYSQL_ROW row1;
			MYSQL_ROW row2;
			char consulta [80];
			
			strcpy (consulta, "SELECT SUM(PUNTUACION.PUNTOS) FROM (JUGADOR,PARTIDA,PUNTUACION)"
					" WHERE (PUNTUACION.ID_J=JUGADOR.IDJ) AND (PUNTUACION.ID_P=PARTIDA.ID)" 
					"AND (JUGADOR.USERNAME='PEPITO')");
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado1 = mysql_store_result (conn);
			row1 = mysql_fetch_row (resultado1);
			
			if (row1== NULL)
				printf("No se han obtenido datos en la consulta\n");
			else
				puntos1 = atoi (row1[0]);
			
			strcpy (consulta, "SELECT SUM(PUNTUACION.PUNTOS) FROM (JUGADOR,PARTIDA,PUNTUACION)"
					" WHERE (PUNTUACION.ID_J=JUGADOR.IDJ) AND (PUNTUACION.ID_P=PARTIDA.ID)" 
					"AND (JUGADOR.USERNAME='JUANITO')");
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado2 = mysql_store_result (conn);
			row2 = mysql_fetch_row (resultado2);
			
			if (row2== NULL)
				printf("No se han obtenido datos en la consulta\n");
			else
				puntos2 = atoi (row2[0]);
			
			if (puntos1<puntos2)
				
				sprintf(respuesta, "2/Juanito es quien mas puntos tiene con %d puntos\n", puntos2);
			else
				sprintf(respuesta, "2/Pepito es quien mas puntos tiene con %d puntos\n", puntos1);
		}
		else if (codigo==3 && encontrado==1)
		{
			MYSQL_RES *resultado9;
			MYSQL_ROW row9;
			int IDJ;
			int id;
			char ganador [20];
			char consulta[80];
			//En primer lugar, preguntamos el nombre de un ganador. 
			//printf("Dame el nombre del ganador\n");
			//scanf("%s",ganador);
			//printf("esta bien");
			//Ahora, realizamos la consulta. En mi caso, queremos el ID del ganador. 
			strcpy (consulta,"SELECT JUGADOR.IDJ FROM (JUGADOR, PARTIDA, PUNTUACION)" 
					"WHERE PARTIDA.GANADOR = '");
			strcat (consulta, nombre);
			strcat (consulta,"'AND PARTIDA.GANADOR=JUGADOR.USERNAME AND PARTIDA.ID=PUNTUACION.ID_P AND PUNTUACION.ID_J=JUGADOR.IDJ");
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			//printf("esta bien");
			//recogemos el resultado de la consulta
			resultado9 = mysql_store_result (conn);
			//printf("Resultado 9: %s\n", resultado9);
			row9 = mysql_fetch_row (resultado9);
			if (row9 == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
				id = atoi (row9[0]);
			//while (row9 !=NULL) {
			
			//la columna 0 contiene tiene el ID del ganador
			sprintf (respuesta,"3/ID del ganador: %d\n", id);
			//obtenemos la siguiente fila
			//row9 = mysql_fetch_row (resultado9);
			
		}
		
		else if (codigo==4)
		{
			//ListaConectados *lista;
			MYSQL_RES *resultado1;
			MYSQL_ROW row1;
			char consulta1[100];
			strcpy(consulta1, "SELECT JUGADOR.USERNAME FROM (JUGADOR)"
				   " WHERE JUGADOR.USERNAME = '");
			strcat (consulta1, nombre);
			strcat (consulta1, "'");
			
			err=mysql_query (conn, consulta1);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado1 = mysql_store_result (conn);
			row1 = mysql_fetch_row (resultado1);
			
			if (row1 == NULL)
				sprintf (respuesta,  "4/Usuario no registrado\n");
			
			else
			{
				MYSQL_RES *resultado2;
				MYSQL_ROW row2;
				char consulta2[100];
				strcpy(consulta2, "SELECT JUGADOR.CONT FROM (JUGADOR)"
					   " WHERE JUGADOR.CONT = '");
				strcat (consulta2, contrasena);
				strcat (consulta2, "' AND JUGADOR.USERNAME='");
				strcat (consulta2, nombre);
				strcat (consulta2, "'");
				
				err=mysql_query (conn, consulta2);
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				resultado2 = mysql_store_result (conn);
				row2 = mysql_fetch_row (resultado2);
				
				if (row2 == NULL)
					sprintf (respuesta, "4/Contraseña incorrecta");
				
				else if (strcmp(row1[0],nombre)==0 && strcmp(row2[0],contrasena)==0)
				{
					
					pthread_mutex_lock( &mutex );//No me interrumpas ahora
					Pon(&lista, nombre, sock_conn);
					pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
					//notificar a todos los clientes conectados
					DameConectados (&lista, conectados);
					
					char notificacion[500];
					
					sprintf (notificacion, "6/%s",conectados);
					printf("%s\n", notificacion);
					int j;
					for (j=0; j< lista.num; j++)
						write (lista.conectados[j].socket, notificacion, strlen(notificacion));
					encontrado=1;
				}
			}
			
		}
		
		
		else if (codigo==5 && encontrado==0)
		{
			char consulta[80];
			int IDJ;
			char idj[80];
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
			
			char consulta5[80];
			MYSQL_RES *resultado5;
			MYSQL_ROW row5;
			strcpy(consulta5, "SELECT JUGADOR.USERNAME FROM (JUGADOR) WHERE JUGADOR.USERNAME = '");
			strcat(consulta5,nombre);
			strcat(consulta5, "';");
			
			
			
			err=mysql_query (conn, consulta5);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado5 = mysql_store_result (conn);
			row5 = mysql_fetch_row (resultado5);
			
			
			if (row5 == NULL)
			{
				char insertar[80];
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
				sprintf(respuesta, "5/Usuario registrado, por favor inicie sesion si quiere hacer una consulta.");
				//encontrado=1;
			}
			else
				sprintf(respuesta, "5/Usuario ya existe, por favor inicie sesión");
		}
		
/*		else if (codigo == 7 && encontrado == 1) {*/
/*			char notificacion[500];*/
/*			sprintf (notificacion, "7/¿Quieres unirte a mi partida?");*/
/*			printf("%s\n", notificacion);*/
/*			int j;*/
/*			for (j=0; j< lista.num; j++)*/
/*				write (lista.conectados[j].socket, notificacion, strlen(notificacion));*/
/*		}*/
		
		else if (codigo == 8 && encontrado == 1) {
			
			char notificacion[500];
			sprintf (notificacion, "8/¿Quieres unirte a la partida de %s?", host);
			pthread_mutex_lock( &mutex );//No me interrumpas ahora
			crearPartida(&partida, host, sock_conn, invitado);
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			//notificar a todos los clientes conectados
			printf("%s\n", notificacion);
			int j;
			int encontrado1=0;
			for (j=0; j< lista.num && encontrado1==0; j++)
			{
				if (lista.conectados[j].nombre==invitado)
				{
					write (lista.conectados[j].socket, notificacion, strlen(notificacion));
					encontrado1=1;
				}
			}
		}
		
		else if (codigo == 9 && encontrado == 1){
			pthread_mutex_lock( &mutex );//No me interrumpas ahora
			estadojugadores(&partida, "Listo");
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			//notificar a todos los clientes conectados
			DameJugadoresyEstado(&partida, estados);
			
			char notificacion[500];
			
			sprintf (notificacion, "9/%s",estados);
			printf("%s\n", notificacion);
			int j;
			for (j=0; j< lista.num; j++)
				write (lista.conectados[j].socket, notificacion, strlen(notificacion));
			
		}
		else if (codigo == 10 && encontrado == 1){
			pthread_mutex_lock( &mutex );//No me interrumpas ahora
			estadojugadores(&partida, "No Listo");
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			//notificar a todos los clientes conectados
			
			
			DameJugadoresyEstado(&partida, estados);
			
			char notificacion[500];
			
			sprintf (notificacion, "9/%s",estados);
			printf("%s\n", notificacion);
			int j;
			for (j=0; j< lista.num; j++)
				write (lista.conectados[j].socket, notificacion, strlen(notificacion));
			
		}
		
		if ((codigo !=0 && codigo !=4 && codigo !=7 && codigo !=8 && codigo !=9 && codigo !=10) || (codigo !=0 && encontrado==0))
		{
			
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
			//write (sock_conn,respuesta1, strlen(respuesta1));
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	mysql_close (conn);
}

int main(int argc, char *argv[])
{
	//lista.num=0;
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
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
	serv_adr.sin_port = htons(9050);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	pthread_t thread;
	i=0;
	// Bucle para atender a 5 clientes
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacerprintf ("Escuchando\n");
		
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;
	}
}
}
