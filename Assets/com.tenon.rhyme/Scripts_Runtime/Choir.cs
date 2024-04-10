using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenonKit.Rhyme {

    public class Choir {

        // Data
        Sheet sheet;

        // State
        bool isEnteringSinging;
        bool isExitingSinging;
        int currentIndex;
        float currentTime;

        public void SetSheet(Sheet sheet) {
            this.sheet = sheet;
            isEnteringSinging = false;
            isExitingSinging = false;
        }

        public void EnterSinging() {
            isEnteringSinging = true;
            isExitingSinging = false;
            currentIndex = 0;
            currentTime = 0;
        }

        public void Sing(float dt) {
            if (isExitingSinging) {
                return;
            }

            var speed = sheet.speed;
            var delay = sheet.delay;

            if (isEnteringSinging) {
                isEnteringSinging = false;
            }

            currentTime += dt;
            currentIndex = Mathf.FloorToInt(currentTime / speed);

            if (currentIndex >= sheet.content.Length) {
                isExitingSinging = true;
            }
        }

    }

}