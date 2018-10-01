using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public interface ICameraState
    {
        void CameraBehavior();
    }

    public class CameraNomalState : ICameraState
    {
        private Animator animator;

        public CameraNomalState(Animator animator)
        {
            this.animator = animator;
        }

        public void CameraBehavior()
        {
            //animator.SetTrigger(0);
        }
    }

    public class CameraSkillActionState : ICameraState
    {
        private Animator animator;

        public CameraSkillActionState(Animator animator)
        {
            this.animator = animator;
        }

        public void CameraBehavior()
        {
            animator.SetTrigger("Skill_1");
        }
    }

    public class CameraShakeState : ICameraState
    {
        private Animator animator;

        public CameraShakeState(Animator animator)
        {
            this.animator = animator;
        }

        public void CameraBehavior()
        {
            animator.SetTrigger("Shake");
        }
    }
}