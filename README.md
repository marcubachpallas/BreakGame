# BreakGame

El proyecto esta basado en crear un juego similar a SmashHit, recreando sobretodo las mecanicas

Para llegar a un sistema similar, y donde tube la mayor complicacion, despues de hacer varias pruevas para romper de una forma correcta la geometria, y no encontrar una forma facil y accesible, opte por preromper los objetos y juntarlos con joints, ya que las otras opciones como, rotura dinamica de la geometria u otras opciones similares no eran factibles, tanto de desarrollar como probablemente de rendimiento.
La opcion utilizada al final es que por codigo se crean joints bidireccionales con todos las piezas que esten en contacto, y despues al disparar, al colisionar destruye estos joints de los objetos colisionados, teniendo unos "anchors" que hacen que el objeto se aguante, una vez una o varias piezas detectan que no tienen ningun anchor conectado a ellos por joints, estas caen para dar un efecto similar al del juego.

Como linias futuras y cosas a mejorar, sobretodo es el tema de level design y arte
