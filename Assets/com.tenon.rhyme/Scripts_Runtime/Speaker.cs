using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenonKit.Rhyme {

    public class Speaker {

        Sheet sheet;

        // State
        bool isEnteringPlaying;
        bool isExitingPlaying;
        int currentIndex;
        float currentTime;

        public void SetSheet(Sheet sheet) {
            this.sheet = sheet;
            isEnteringPlaying = false;
            isExitingPlaying = false;
        }

        public void EnterPlaying() {
            isEnteringPlaying = true;
            isExitingPlaying = false;
            currentIndex = 0;
            currentTime = 0;
        }

        public void ExitPlaying() {
            isExitingPlaying = true;
        }

        public char GetCurrentContent() {
            return sheet.content[currentIndex];
        }

        public void ApplyPlay(float dt) {
            if (isExitingPlaying) {
                return;
            }

            var speed = sheet.playingSpeed;

            if (isEnteringPlaying) {
                isEnteringPlaying = false;
            }

            currentTime += dt;
            currentIndex = Mathf.FloorToInt(currentTime / speed);

            // if (currentIndex >= sheet.content.Length) {
            //     isExitingPlaying = true;
            // }
        }

    }

}