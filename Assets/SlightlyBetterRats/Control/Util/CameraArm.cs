using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR {
    public class CameraArm : BasicMotor<Channels> {
        public bool useControlRotation = true;
        public string rotationChannel = "rotation";
        private Quaternion rot;

        private void LateUpdate() {
            if (useControlRotation && channels != null) {
                transform.rotation = rot;
            }
        }

        public override void TakeInput() {
            rot = channels.GetQuaternion(rotationChannel);
        }
    }
}