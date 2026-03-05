namespace com.ab.complexity.core
{
    public class MovementEntryDef : StaticEntryDef, 
        IStaticRegisterTypeDef, IStaticUpdateDef
    {
        public void RegisterType()
        {
            W.RegisterComponentType<MovementEntry>();
            W.RegisterComponentType<MovementSamePosition>();
            
            W.RegisterTagType<Movement>();
        }

        public void RegisterUpdate()
        {
            Sys.AddUpdate(new MovementInitSystem());
            Sys.AddUpdate(new MovementVelocitySystem());
            Sys.AddUpdate(new MovementUpdatePositionSystem());
            Sys.AddUpdate(new MovementSamePositionSystem());
            Sys.AddUpdate(new MovementDirectionSystem());
            Sys.AddUpdate(new MovementAnimationLocomotionSystem());
        }
    }
}