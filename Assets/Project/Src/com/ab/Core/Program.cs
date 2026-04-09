using UnityEngine;
using FFS.Libraries.StaticEcs;

namespace com.ab.complexity.core
{
    public class Program {
        public static void Main() {
            // Creating world data
            W.Create(WorldConfig.Default());
            
            // Initializing the world
            W.Initialize();
            
            // Creating systems
            Sys.Create();
            SysReg.Add(new MovementVelocitySystem());
    
            // Initializing systems
            Sys.Initialize();
    
            // Creating entity
            // var entity = W.Entity.New(
                // new Velocity { Value = 1f },
                // new Position { Value = Vector3.zero },
                // new Direction { Value = Vector3.one }
            // );
            
            // Update all systems - called in every frame
            // World<>.Systems<>.Update();
            // Destroying systems
            // World<>.Systems<>.Destroy();
            // Destroying the world and deleting all data
            W.Destroy();
            
            
             // foreach (var entity in W.Query.Entities<All<Position, Velocity, Direction>>()) {
        }
    }
}