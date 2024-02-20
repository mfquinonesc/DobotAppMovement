

# C# API DRIVER  DOBOT MAGICIAN 

Se debe bajar la versión del fabricante c# demo y copiar la carpeta CPlusDll en el 
proyecto porque esta carpeta contiene el Driver que funciona.

En la carpeta CPluDll se encuentran las clases DobotDll.cs y DobotDllType.cs

La clase DobotDll contiene los métodos necesarios para la operacion del Dobot Magician
esta clase inporta 


1. Conección del dobot:

Se usa el metodo:

```
[DllImport("DobotDll.dll", EntryPoint = "ConnectDobot", CallingConvention = CallingConvention.Cdecl)]
public static extern int ConnectDobot(string portName, int baudrate, StringBuilder fwType, StringBuilder version);

StringBuilder fwType = new StringBuilder(60);
StringBuilder version = new StringBuilder(60);

int ret = DobotDll.ConnectDobot("", 115200, fwType, version);
if (ret != (int)DobotConnect.DobotConnect_NoError)            {
    
    // El error 2  quiere decir que ya se esta ejecutando
}
else
{
    // coneccion exitosa
}
```


Si la respuesta de del metodo de coneccion es 0 cuando se conecta exitosamente.


2. Establecer el HOME del Dobot. Para  esto se utiliza la función:

```
[DllImport("DobotDll.dll", EntryPoint = "SetHOMECmd", CallingConvention = CallingConvention.Cdecl)]
public static extern int SetHOMECmd(ref HOMECmd homeCmd, bool isQueued, ref UInt64 queuedCmdIndex);
```
Esta funcion recibe como parametros:

```
HOMECmd homeCmd;
homeCmd.temp = 0;
bool isQueued = false;
UInt64 cmdIndex = 0;
DobotDll.SetHOMECmd(ref homeCmd, isQueued, ref cmdIndex);
```

Esta funcion recibe tres parametros HOMECmd,isQueued, cmdIndex;


3. Obtencion de los datos de la posicion de HOME del Robot
   
```
[DllImport("DobotDll.dll", EntryPoint = "SetHOMECmd", CallingConvention = CallingConvention.Cdecl)]
public static extern int SetHOMECmd(ref HOMECmd homeCmd, bool isQueued, ref UInt64 queuedCmdIndex);
```

* Los valores que retorna la funcion por defecto son:
```
x = 250
y = 0 
z = 50
r = 0

HOMEParams homeparams;
homeparams.r = 0;
homeparams.z = 0;
homeparams.y = 0;
homeparams.x = 0;
DobotDll.GetHOMEParams(ref homeparams);
```





4. Establecer lor parametros del HOME
   
```
[DllImport("DobotDll.dll", EntryPoint = "SetHOMEParams", CallingConvention = CallingConvention.Cdecl)]
public static extern int SetHOMEParams(ref HOMEParams homeParams, bool isQueued, ref UInt64 queuedCmdIndex);
```
```
x = 250
y = 0 
z = 50
r = 0

UInt64 cmdIndex = 0;
HOMEParams homeparams;
homeparams.r = 0;
homeparams.z = 50;
homeparams.y = 0;
homeparams.x = 250;
DobotDll.SetHOMEParams(ref homeparams,false,ref cmdIndex);
```

El anterior método es capaz de modificar la posicion inicial del dobot. Los valores (r,x,y,z) son los que trae por defecto el dobot 

5. Función para desconectar el dobot 
```
[DllImport("DobotDll.dll", EntryPoint = "DisconnectDobot", CallingConvention = CallingConvention.Cdecl)]
public static extern void DisconnectDobot();
```
Esta función no requiere de parametros.
```
DobotDll.DisconnectDobot();      
```





            










