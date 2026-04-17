using com.ab.common;
using com.ab.complexity.core;

namespace com.ab.domain.joystick
{
    public class JoystickAttachMono : EntityLink
    {
        protected override void Register()
        {
            Ent.Set<JoystickAttachTag>();
        }
    }
}