namespace com.ab.common.Camera
{
    public class CameraRefMono : EntityLink
    {
        public UnityEngine.Camera Camera;

        protected override void Register()
        {
            Ent.Add(new CameraRef(this));
            base.Register();
        }
    }
}