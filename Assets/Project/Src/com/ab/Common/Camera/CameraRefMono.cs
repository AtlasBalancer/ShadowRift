namespace com.ab.common.Camera
{
    public class CameraRefMono : EntityLink
    {
        public UnityEngine.Camera Camera;

        protected override void Register()
        {
            Ent.Set(new CameraRef(this));
            base.Register();
        }
    }
}