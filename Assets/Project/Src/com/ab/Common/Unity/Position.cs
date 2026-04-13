using System;
using UnityEngine;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticPack;

namespace com.ab.complexity.core
{
    public struct Position : IComponent
    {
        public Vector2 Val;

        public static readonly ComponentTypeConfig<Position> Config = new(
            guid: new Guid("346a1ca4076a4e6291526c1de698b454")
        );

        public Position(Vector3 val) => Val = val;

        public void Write<TWorld>(ref BinaryPackWriter writer, World<TWorld>.Entity self)
            where TWorld : struct, IWorldType
        {
            writer.WriteFloat(Val.x);
            writer.WriteFloat(Val.y);
        }

        public void Read<TWorld>(ref BinaryPackReader reader, World<TWorld>.Entity self, byte version, bool disabled)
            where TWorld : struct, IWorldType
        {
            var x = reader.ReadFloat();
            var y = reader.ReadFloat();

            Val = new Vector2(x, y);
        }
    }
}