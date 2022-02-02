# 東方渡霊録～Tragedies in Vacuous Scroll

## NOTE ON BULLET SYSTEM

The philosophy of the bullet system is assuming each bullet can change its behavior during its lifetime, therefore allowing bullets to have complex patterns.

A behavior of the bullet is by its nature a bool function with no parameters (bool BulletBehavior()). Each bullet will have a list of BulletBehavior that the bullet can iterate through. The bullet remembers its current behavior by using a integer field, equals to the corresponding index of the behavior. In each update (frame), the bullet checks whether it is out of screen boundaries and if not, it will invoke the behavior function. When BulletBehavior returns true, the bullet will seek the next behavior in the list therefore changing the behavior. Note that currently the bullet will halt when index is out of bound.

### BulletBehavior is an effect-over-time, why not using Coroutines?

It is possible to use coroutines, but since a bullet should detect whether it is out of boundaries each frame, in each coroutine function, you must also check this (=> you will have an extra IF to write for every WHILE or FOR you write). What's more, coroutines won't reduce the burden when writing timed behaviours. You still have to expose the time limit via field or property. Finally, although the use case is still unknown, you won't be able to use a "lifetime function" (executed every frame independant to the behaviors).

### Implementation of BulletLauncher

A BulletLauncher defines how a group of bullet is laid out, and providing initialization information to the bullets. BulletLauncher should call the Launch() method of the bullet it launches. The signature of the Launch method is different, depending on the type of the bullet. Using interfaces may alow a BulletLauncher to launch bullet types with similar properties instead of launching one single type (class). When BulletPool is implemented, the Launch method should also reset the bullet. Note that the Launch method is NOT in the Bullet base class since its signature is not unique.
