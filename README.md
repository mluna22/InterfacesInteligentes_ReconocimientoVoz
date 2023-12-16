# Interfaces Inteligentes
## Reconocimiento de voz

1. Se genera apk con el ejemplo de la API de Hugging Face para Unity.

![demo](demos/ej1.gif)

2. Al tocar una araña:
    - Se marca la araña seleccionada con un outline blanco.
    - Comienza la grabación de voz por 3 segundos.
    - Se envía el audio a la API de Hugging Face.
    - Se muestra el texto reconocido.
    - La araña seleccionada ejecuta la acción correspondiente (jump o flip).

![demo](demos/ej2.gif)