# ASTEROID REBIRTH: Neon Legacy

## Descripción
Juego tipo arcade inspirado en Asteroids, desarrollado en Unity. Controla una nave en un campo de asteroides, destruye y esquiva, usa habilidades especiales y enfréntate a un jefe final.

## Características principales
- **Movimiento de nave:**
  - Giro rápido (A/D o ←/→)
  - Aceleración y frenado realistas (W/S o ↑/↓)
  - Teletransporte (Blink) con cooldown
- **Disparo:**
  - Balas rápidas, tamaño configurable, sin límite de disparo
  - Destruyen asteroides al impactar
  - Si dos balas chocan, ambas desaparecen
- **Asteroides:**
  - Tres tamaños: grande, mediano, pequeño
  - Fragmentación procedural: grandes generan dos medianos, medianos uno pequeño
  - Velocidad y tamaño configurables
  - Siempre se mueven hacia el centro
- **Spawner:**
  - Asteroides aparecen desde fuera del área de juego, uno cada 10 segundos
  - Nunca aparecen cerca del jugador
- **Jefe:**
  - Aparece al llegar a 1000 puntos
  - Dispara patrones de balas
- **UI:**
  - Canvas con puntaje, combo, vidas, barra de energía
- **Tags y prefabs:**
  - Player, Asteroid, AsteroidMedium, AsteroidSmall, Bullet, Boss
  - Prefabs bien configurados y enlazados

## Estructura de carpetas
- `Assets/Prefabs/` — Prefabs de nave, asteroides, balas, jefe
- `Assets/Scripts/` — Scripts principales (`PlayerController`, `Asteroid`, `Bullet`, `AsteroidSpawner`, `GameManager`, etc.)
- `Assets/Sprites/` — Sprites de nave, asteroides, balas, fondo
- `Assets/Scenes/` — Escena principal

## Configuración recomendada
- **Nave:** Rigidbody2D (Dynamic), Collider2D, script `PlayerController`
- **Asteroides:** Rigidbody2D (Dynamic), Collider2D, script `Asteroid`, tags y prefabs enlazados
- **Bala:** Rigidbody2D (Dynamic), Collider2D, script `Bullet`, tag `Bullet`, tamaño y velocidad configurables
- **Spawner:** Script `AsteroidSpawner`, referencia al prefab de asteroide grande y al jugador
- **GameManager:** Script `GameManager`, referencias a UI y spawner

## Notas
- No es necesario subir este proyecto a GitHub.
- Todos los valores de tamaño, velocidad y lógica pueden ajustarse desde el Inspector.
- El código está modularizado y documentado para facilitar cambios y ampliaciones.

---
¡Disfruta programando y jugando tu versión de Asteroids!
