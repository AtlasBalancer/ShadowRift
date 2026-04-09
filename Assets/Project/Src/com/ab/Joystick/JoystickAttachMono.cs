using com.ab.common;

namespace com.ab.complexity.core
{
    public class JoystickAttachMono : EntityLink
    {
        protected override void Register() => 
            Ent.Set<JoystickAttachTag>();
    }
}