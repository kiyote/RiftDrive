Game start
-Players name their mothership
-Comes with a few mothership modules (hanger, cryopod storage, etc)
-Players get a starting ship
-Comes with a few ship modules (bridge, engine, etc)


Actor
Discipline: How many cards you can hold
Expertise: How many cards per turn you can play
Training: How many cards you can draw

Concepts

Focus
A particular ability, for example, engineering/command/etc

Skill
A bundle of focus, for example a skill may offer 3xCommand-1 focus cards.


Code Layering
Controller - Sanitizes web input, calls through to manager, may not apply business logic
Manager - High-level single domain responsibility, aggregates functionality across services, allowed to apply business logic
Service - Low-level single domain responsibility, aggregates functionality across repositories, allowed to apply domain logic
Repository - Dumb read/write operations to a persistence layer, may not apply business logic
Model - Purely data buckets and data structure, may not apply business logic
