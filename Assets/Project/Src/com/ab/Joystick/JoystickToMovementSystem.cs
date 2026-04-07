using System;
using UnityEngine;
using FFS.Libraries.StaticEcs;
using Object = UnityEngine.Object;

namespace com.ab.complexity.core
{
    public struct JoystickToMovementSystem : IInitSystem, IUpdateSystem
    {
        Context _def;
        UltimateJoystick _joystick;

        public void Init()
        {
            _def = Context.ContextGet();
            _joystick = Object.Instantiate(_def.JoystickPrefab, _def.Root);
        }

        public void Update()
        {
            Vector2 dir;
            Vector2 velocity;
            float magnitude;

            if (_joystick.InputActive)
            {
                 dir = new Vector2(
                    _joystick.GetHorizontalAxisRaw(),
                    _joystick.GetVerticalAxisRaw());

                 velocity = new Vector2(
                    _joystick.GetHorizontalAxis(),
                    _joystick.GetVerticalAxisRaw());

                 magnitude = velocity.magnitude;
            }
            else
            {
                dir = Vector2.zero;
                velocity = Vector2.zero;
                magnitude = 0;
            }
            
            TagAll<JoystickAttachTag> all = default;
            All<Direction, Velocity> filter = default;
            var with = With.Create(all, filter);

            foreach (var item in W.Query.Entities(with))
            {
                // Debug.Log($"Input raw:{_joystick.GetVerticalAxisRaw()}");

                item.Ref<Direction>().Value = dir;
                ref var velocityRef = ref item.Ref<Velocity>();
                velocityRef.Value = velocity;
                velocityRef.Magnitude = magnitude;

                if (_def.Debug)
                    Debug.Log($"{nameof(JoystickToMovementSystem)}::{nameof(Update)} =>\n" +
                              $"interactable: {_joystick.Interactable}, input active: {_joystick.InputActive},\n" +
                              $"ha: {_joystick.HorizontalAxis}, gha: {_joystick.GetHorizontalAxis()}, ghar: {_joystick.GetHorizontalAxisRaw()}");
            }
        }

        [Serializable]
        public class Context : IStaticContextDef<Context>
        {
            public bool Debug;
            public Transform Root;
            public UltimateJoystick JoystickPrefab;
        }
    }
}