# 東方渡霊録～Tragedies in Vacuous Scroll

## NOTE ON BULLET SYSTEM

The philosophy of the bullet system is assuming each bullet can change its behavior during its lifetime, therefore allowing bullets to have complex patterns.

A behavior of the bullet is by its nature a bool function with no parameters (bool BulletBehavior()). Each bullet will have a list of BulletBehavior that the bullet can iterate through. The bullet remembers its current behavior by using a integer field, equals to the corresponding index of the behavior. In each update (frame), the bullet checks whether it is out of screen boundaries and if not, it will invoke the behavior function. When BulletBehavior returns true, the bullet will seek the next behavior in the list therefore changing the behavior. Note that currently the bullet will halt when index is out of bound.

### BulletBehavior is an effect-over-time, why not using Coroutines?

It is possible to use coroutines, but since a bullet should detect whether it is out of boundaries each frame, in each coroutine function, you must also check this (=> you will have an extra IF to write for every WHILE or FOR you write). What's more, coroutines won't reduce the burden when writing timed behaviours. You still have to expose the time limit via field or property. Finally, although the use case is still unknown, you won't be able to use a "lifetime function" (executed every frame independant to the behaviors).

### Implementation of BulletLauncher

A BulletLauncher defines how a group of bullet is laid out, and providing initialization information to the bullets. BulletLauncher should call the Launch() method of the bullet it launches. The signature of the Launch method is different, depending on the type of the bullet. Using interfaces may alow a BulletLauncher to launch bullet types with similar properties instead of launching one single type (class). When BulletPool is implemented, the Launch method should also reset the bullet. Note that the Launch method is NOT in the Bullet base class since its signature is not unique.


## INSTRUCTION TO HIT INTERACTION ##

#### 1. Collision is triggered when a bullet enters the hitbox of Youmu ####

This part is partially implemented, and can be found in OnTriggerEnter method of YoumuController. The event "onYoumuHit" will be invoked if the condition is met. However, there is a twist.

#### 2. In the next 30 frames, player may press C button to trigger spell card and neglate the hit ####

This detection window should be opened before onYoumuHit is invoked. There are several things to consider here:

1. How to count the frames? 

You may use a coroutine to test a flag (e.g. bool isInSPEffect, initially false), and when ever the player presses the C button, the flag is changed to True. This means:

```C#
for(var i=0; i<detectionWindowLength; i++){
  if(flag){
    /*Invoke sp event
    ...
    */
    yield break; //This will stop the coroutine so onYoumuHit will not be invoked.
  }
  yield return;
}
onYoumuHit?.invoke();
```

2. Prevent another hit from being registered. This should be straight forward, by checking a condition in OnTriggerEnter function.

3. Which class should be responsible for this detection? It might be more reasonable to move this to another class. But onYoumuHit can only be invoked inside its own class, you may need to expose it.  But the refactoring decision is up to you.

#### 3. If a onYoumuHit is triggered, several effects will kick in ####

1. Youmu should be invincible for like 5 secs (or you could count this by frame), meaning no hit should be registered during this period. You may setup a timer and add a condition in OnTriggerEnter. You may also want to move this functionality to another class and make that class subscribe to onYoumuHit.

2. All bullets on the screen should be returned to bulletpool. It might be good, when getting a bullet from the bulletpool, to reassign its parent (e.g. from bulletpool to bulletlauncher, calling bullet.transform.SetParent(Transform parent), then all the children of bulletlauncher will be bullets on the screen and you can iterate through them with bulletLauncher.transform.GetComponentsInChildren<Type of Bullet>(), invoking thier onBulletDeathManual event. When returning the bullet, you will also need to reassign the parent to bulletPool. 

3. A more interesting effect can be achieved by using an object with circle collider 2D as trigger. Set its initial radius to 0 and deactivate it. When a wiping effect is invoked, set it to active and expand its radius (for a duration of time). whenever a bullet enters the circle collider, this object calls the bullet's onBulletDeathManual. You may test with other effect e.g. wipe from bottom to top. After the end of the effect, reset the object.
