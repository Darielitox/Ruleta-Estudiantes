# 🎰 Ruleta Aleatoria de Estudiantes

Aplicación de consola desarrollada en **C# (.NET)** que selecciona estudiantes aleatoriamente y les asigna roles de forma equitativa, evitando repeticiones hasta que todos hayan participado.

---

## 📋 Descripción

Esta herramienta fue diseñada para el aula de clases. Permite girar una "ruleta" que selecciona estudiantes al azar y les asigna roles predefinidos (como *Desarrollador en Vivo* o *Facilitador de Ejercicio*). El sistema lleva un registro interno para garantizar que ningún estudiante repita un rol hasta que todos hayan pasado.

---

## ✨ Características

- 🎰 **Ruleta animada** con efecto de giro en consola
- 🔁 **Sin repeticiones** — el sistema rastrea quién ya fue seleccionado por rol
- 👨‍🎓 **Administración de estudiantes** — agregar, modificar y eliminar
- 🔑 **Administración de roles** — crear y gestionar roles personalizados
- 📄 **Historial de selecciones** guardado en archivo `.txt`
- 🔄 **Reinicio de sesión** para comenzar un nuevo ciclo
- 🎵 Efectos de sonido en la animación (`.wav`)
- 🌈 Interfaz colorida con banner ASCII art

---

## 🗂️ Estructura del Proyecto

```
Ruleta_Estudiantes/
├── Program.cs          # Punto de entrada y menú principal
├── Ruleta.cs           # Lógica de la ruleta y animación
├── Metodos.cs          # Gestión de estudiantes, roles e historial
├── GUI.cs              # Interfaz gráfica de consola (menús, banner)
├── estudiantes.txt     # Lista de estudiantes (generado automáticamente)
├── roles.txt           # Lista de roles (generado automáticamente)
├── Historial de selecciones.txt  # Registro de sorteos
└── audio/
    ├── ruleta_giro.wav
    ├── ganador.wav
    └── ganadoresGenerales.wav
```

---

## ▶️ Cómo Ejecutar

### Requisitos
- [.NET SDK](https://dotnet.microsoft.com/download) 6.0 o superior
- Sistema operativo Windows (por el uso de `System.Media.SoundPlayer`)

### Pasos

```bash
# Clonar el repositorio
git clone https://github.com/Darielitox/ruleta-estudiantes.git
cd ruleta-estudiantes

# Compilar y ejecutar
dotnet run
```

> ⚠️ Asegúrate de tener la carpeta `audio/` con los archivos `.wav` en el directorio raíz del proyecto. Sin ellos, los sonidos se omiten silenciosamente.

---

## 🎮 Uso

| Opción | Descripción |
|--------|-------------|
| 🎰 Iniciar Ruleta | Gira y asigna roles a estudiantes aleatoriamente |
| 📄 Historial | Ver o vaciar el historial de selecciones |
| 🎓 Administrar Estudiantes | Agregar, modificar o eliminar estudiantes |
| 🔑 Administrar Roles | Agregar, modificar o eliminar roles |
| 🔄 Reiniciar Sistema | Limpia el registro de la sesión actual |

**Navegación:** `↑ ↓` o `W S` para moverse, `Enter` para seleccionar, `Esc` para salir.  
Durante la animación, presiona `O` para saltar al resultado directamente.

---

## 📁 Archivos Generados

Al ejecutar por primera vez, se crean automáticamente:

- `estudiantes.txt` — con estudiantes de ejemplo
- `roles.txt` — con dos roles por defecto
- `Historial de selecciones.txt` — donde se registran los sorteos

---

## 👤 Autor

**Dariel De Jesus** — 2025-2155 - Estudiante de Seguridad Informatica ITLA 

---

## 📄 Licencia

Este proyecto es de uso libre para fines educativos.
