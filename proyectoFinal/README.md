# tl1-proyectofinal2024-leonardo4390
# Weather Clash
## The Mythical Duels

### Breve Descripción Del Juego
Es un Juego épico, donde los climas y estado del día se alzan como poderosos aliados o formidables adversarios. En este juego los personajes son seres míticos que personifican los diferentees estados del clima y estado del dia. Cada uno tiene habilidades únicas basados en su clima o estado del dia asociado.

### Jugabilidad Descripción
El juego consiste en un combate entre dos, donde el usuario deberá elegir un personaje a su preferencia y el adversario será elegido aleatoriamente. Las características de los personajes(Fuerza, Velocidad, Armadura,Destreza,Nivel) tanto para el usuario como para el adversario será creado de forma aleatoria, Para asegurar paridad la salud inicialmente sera  para todos en 100. La batalla será por turnos, uno ataca y el otro defiende hasta que la salud de uno de ellos termine en 0  o menor. La forma de calcular el daño provocado en cada turno es la siguiente:

• Ataque: Destreza * Fuerza * Nivel (del personaje que ataca)
• Efectividad: Valor aleatorio entre 1 y 100.
• Defensa: Armadura * Velocidad (del personaje que defiende)
• Constante de Ajuste: 500
Daño provocado =((Ataque * Efectividad) - Defensa)/Constante de Ajuste

Una vez que termine el turno de personaje que ataca, la salud del personaje que defiende se corrige a: 
Salud = Salud – Daño provocado

En la batalla si el clima o estado del dia coincide con el beneficio asociado al personaje atacante habra un incremente en el daño provocado de forma aleatoria, por ejemplo si el beneficio del personaje es Lluvia y el estado del dia o el clima coincide se generara puntos de daños de beneficios de forma aleatoria y se le sumará a daños provocados, este beneficio será por única vez, es decir en una unica vuelta de turno. Una ves terminado la batalla al ganador se le dara un bono de incremento que sera aleatoriamente en una de sus caracteristicas.

### Personajes
1. Tipo: Arquero 
   Nombre: Hydron
   Apodo: El Preciso Húmedo
   Beneficio del Clima: *Lluvia
   Habilidades:
        Flecha Acuática: Sus flechas pueden atravesar la lluvia sin perder velocidad.
        Mirada Líquida: Puede ver claramente a través de la lluvia intensa.
   Descripción: En el campo de batalla, Hydron se convierte en un danzante acuático, moviéndose con gracia entre las gotas de lluvia. Su precisión es legendaria, y sus enemigos nunca saben cuándo serán alcanzados por una flecha que parece surgir del mismo cielo lluvioso.

2. Tipo: Mago 
   Nombre: Pyrus
   Apodo: El Ardiente Sabio
   Beneficio del Clima: *Soleado
   Habilidades:
        Ola de Calor: Emite una onda expansiva que deshidrata a sus enemigos.
        Espejismo Mágico: Crea ilusiones ópticas en el aire caliente.
   Descripción: En el campo de batalla, Pyrus es un estratega astuto. Utiliza el calor como su aliado, cegando a sus oponentes con destellos de luz y desgastándolos hasta que se rindan ante el ardor incesante.

3. Tipo: Guerrero
   Nombre: Durandal
   Apodo: El Señor del Viento
   Beneficio: *Dias Ventosos
   Habilidades: 
        Cuchilla Afilada: Puede crear cuchillas de viento afiladas como navajas.
	    Grito del Vendaval: Puede liberar un grito ensordecedor que desequilibra a sus oponentes.
	    Invocación de Tornados: En momentos de gran necesidad, Puede invocar un tornado masivo.  
   Descripción:En la batalla, Durandal es impredecible. Puede desaparecer en un torbellino y reaparecer detrás de sus enemigos, o       elevarse en el aire y lanzar su espada como un relámpago. Su lema es simple: "Cuando los vientos rugen, yo también lo hago".

4. Tipo: Nigromante
   Nombre: Nekros
   Apodo: El Amo de las Sombras
   Beneficio: *Noche
   Habilidades:
        Resurrección Profana: Nekros puede revivir a los muertos, convirtiéndolos en sus leales sirvientes.
	    Manto de Sombras: Nekros se envuelve en un manto oscuro que lo hace invisible para los ojos mortales. Puede moverse sin ser detectado y emboscar a sus enemigos desde las sombras.
   Descripción: Se dice que Nekros fue una vez un poderoso hechicero que desafió a la muerte misma. En su búsqueda de conocimiento prohibido, cruzó el umbral entre la vida y la muerte. Ahora, camina en ambos mundos, atrayendo a los espíritus errantes y manipulando las energías de la oscuridad.

5. Tipo: Bárbaro
   Nombre: Grom
   Apodo: El Deborador del Sol
   Beneficio: *Dias Soleados
   Habilidades:
	    Espada Solar: Cuando la desenvaina, su filo se calienta, y cada golpe quema como el sol abrasador.
	    Grito Solar: Grom puede rugir como un sol en su cenit. Su grito desorienta a los enemigos, cegándolos 			     momentáneamente y debilitando su voluntad de luchar.
   Descripción: Se dice que Grom nació durante un eclipse solar, y desde entonces, ha estado ligado al sol de una manera mística. Los aldeanos lo ven como un protector divino, un guerrero bendecido por los dioses solares.

6. Tipo: Asesino
   Nombre: Lurker
   Apodo: El Fantasma de la Noche
   Beneficio: *Noche
   Habilidades: 
        Sigilo avanzado, habilidades de camuflaje en la oscuridad, maestro en el uso de armas arrojadizas y técnicas de asesinato silencioso.
   Descripción: Lurker es un enigma. Nadie sabe de dónde viene ni quién lo entrenó en las artes oscuras del asesinato. Su rostro está siempre oculto bajo una capucha negra, y sus ojos brillan como dos luciérnagas en la noche.

7. Tipo: Bruja
   Nombre: Morgana
   Apodo: La Reina del Rayo
   Beneficio: *Dias de Tormenta
   Habilidades: 
        Control sobre los rayos y truenos, invocación de relámpagos, capacidad para electrificar y manipular objetos metálicos.
   Descripción: En la batalla, Morgana es una fuerza imparable. Sus enemigos son reducidos a cenizas por los rayos que ella dirige. Su risa es como el retumbar de un trueno, y su mirada, fría como el metal.

8. Tipo: Hechicero
   Nombre: Mistralia
   Apodo: Susurro velado
   Beneficio: *Dias de Niebla
   Habilidades: 
	    Encantamientos Nebulosos: Mistralia puede inscribir runas en la niebla y lanzar hechizos a través de ella. Puede alterar la realidad temporal o cambiar la percepción de los demás.
	    Camuflaje de Bruma: Se vuelve invisible dentro de la niebla.
        Susurros Nebulosos: Puede enviar mensajes secretos a través de la niebla.
   Descripción: En la batalla, Mistralia es una maestra de la ilusión. Sus enemigos ven sombras que no pueden tocar y escuchan susurros que los confunden. Su risa es como el viento que se desliza entre los árboles.

### Iniciativa del Juego
La idea surge de implementar un poco del gusto de personajes miticos y juegos de la actualidad que aprovechan el estado del tiempo o la hora del día, me pareció adecuado aprovechar esas ideas e implementarlo a este proyecto de combate, sin nada mas que decir.

**Prepárate para explorar un mundo donde los climas y estado del dia cobran vida y los mitos se entrelazan con la naturaleza! 
