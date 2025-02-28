using System;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class GateSpawnerData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public GateTypeId GateTypeId;


        public GateSpawnerData(Vector3 position, Quaternion rotation, GateTypeId gateTypeId)
        {
            Position = position;
            Rotation = rotation;
            GateTypeId = gateTypeId;
        }
    }
}
