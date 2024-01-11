﻿// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System.Collections.Generic;
using Doozy.Runtime.Reactor;
using Doozy.Runtime.Reactor.Animations;
using Doozy.Runtime.Reactor.Reflection;
using Doozy.Runtime.Reactor.Ticker;
using UnityEngine;

namespace Doozy.Runtime.UIManager.Animators
{
    /// <summary>
    /// Specialized animator component used to animate a float value
    /// by listening to a UIContainer (controller) show/hide commands.
    /// </summary>
    [AddComponentMenu("Doozy/UI/Containers/Animators/UIContainer Float Animator")]
    public class UIContainerFloatAnimator : BaseUIContainerAnimator
    {
        /// <summary> Float value target accessed via reflection </summary>
        public ReflectedFloat ValueTarget = new ReflectedFloat();

        /// <summary> Check if the value target is set up correctly </summary>
        public bool isValid => ValueTarget.IsValid();

        [SerializeField] private FloatAnimation ShowAnimation;
        /// <summary> Container Show Animation </summary>
        public FloatAnimation showAnimation => ShowAnimation ?? (ShowAnimation = new FloatAnimation(ValueTarget));

        [SerializeField] private FloatAnimation HideAnimation;
        /// <summary> Container Hide Animation </summary>
        public FloatAnimation hideAnimation => HideAnimation ?? (HideAnimation = new FloatAnimation(ValueTarget));

        #if UNITY_EDITOR
        protected override void Reset()
        {
            ValueTarget ??= new ReflectedFloat();
            base.Reset();
            ResetAnimation(showAnimation);
            ResetAnimation(hideAnimation);
        }
        #endif
        
        protected override void Awake()
        {
            UpdateSettings();
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ShowAnimation?.Recycle();
            HideAnimation?.Recycle();
        }

        /// <summary> Connect to Controller </summary>
        protected override void ConnectToController()
        {
            base.ConnectToController();
            if (!controller) return;

            controller.showReactions.Add(showAnimation.animation);
            controller.hideReactions.Add(hideAnimation.animation);
        }

        /// <summary> Disconnect from Controller </summary>
        protected override void DisconnectFromController()
        {
            base.DisconnectFromController();
            if (!controller) return;

            controller.showReactions.Remove(showAnimation.animation);
            controller.hideReactions.Remove(hideAnimation.animation);
        }

        /// <summary> Play the show animation </summary>
        public override void Show()
        {
            if (reversingShow)
            {
                showAnimation.OnFinishCallback.RemoveListener(OnReverseShowComplete);
                reversingShow = false;
            }
            showAnimation.Play(PlayDirection.Forward);
        }

        /// <summary> Reverse the show animation (if playing) </summary>
        public override void ReverseShow()
        {
            if (showAnimation.isPlaying)
            {
                showAnimation.OnFinishCallback.AddListener(OnReverseShowComplete);
                showAnimation.Reverse();
                reversingShow = true;
                return;
            }
            Hide();
        }

        private void OnReverseShowComplete()
        {
            InstantHide();
            showAnimation.OnFinishCallback.RemoveListener(OnReverseShowComplete);
            reversingShow = false;
        }

        /// <summary> Play the hide animation </summary>
        public override void Hide()
        {
            if (reversingHide)
            {
                hideAnimation.OnFinishCallback.RemoveListener(OnReverseHideComplete);
                reversingHide = false;
            }
            hideAnimation.Play(PlayDirection.Forward);
        }

        /// <summary> Reverse the hide animation (if playing) </summary>
        public override void ReverseHide()
        {
            if (hideAnimation.isPlaying)
            {
                hideAnimation.OnFinishCallback.AddListener(OnReverseHideComplete);
                hideAnimation.Reverse();
                reversingHide = true;
                return;
            }
            Show();
        }
        
        private void OnReverseHideComplete()
        {
            InstantShow();
            hideAnimation.OnFinishCallback.RemoveListener(OnReverseHideComplete);
            reversingHide = false;
        }

        /// <summary> Set show animation's progress at one </summary>
        public override void InstantShow() =>
            showAnimation.SetProgressAtOne();

        /// <summary> Set hide animation's progress at one </summary>
        public override void InstantHide() =>
            hideAnimation.SetProgressAtOne();

        /// <summary> Set the value target </summary>
        /// <param name="reflectedValue"> Reflected value target </param>
        private void SetTarget(object reflectedValue) =>
            SetTarget(reflectedValue as ReflectedFloat);

        /// <summary> Set the value target </summary>
        /// <param name="reflectedValue"> Reflected value target </param>
        private void SetTarget(ReflectedFloat reflectedValue)
        {
            showAnimation.SetTarget(reflectedValue);
            hideAnimation.SetTarget(reflectedValue);
        }

        /// <summary> Refresh the set target and, if the animation is playing, update the calculated values </summary>
        public override void UpdateSettings()
        {
            SetTarget(ValueTarget);
            if (showAnimation.isPlaying) showAnimation.UpdateValues();
            if (hideAnimation.isPlaying) hideAnimation.UpdateValues();
        }

        /// <summary> Stop all animations </summary>
        public override void StopAllReactions()
        {
            showAnimation.Stop();
            hideAnimation.Stop();
        }

        /// <summary> Reset all the reactions to their initial values (if the animation is enabled) </summary>
        /// <param name="forced"> If true, forced will ignore if the animation is enabled or not </param>
        public override void ResetToStartValues(bool forced = false)
        {
            if (showAnimation.isActive) showAnimation.Stop();
            if (hideAnimation.isActive) hideAnimation.Stop();

            showAnimation.ResetToStartValues(forced);
            hideAnimation.ResetToStartValues(forced);

            if (ValueTarget == null || !ValueTarget.IsValid())
                return;

            ValueTarget.SetValue(showAnimation.startValue);

            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(rectTransform);
            UnityEditor.SceneView.RepaintAll();
            #endif
        }
        
        /// <summary> Set animation heartbeat </summary>
        public override List<Heartbeat> SetHeartbeat<T>()
        {
            var list = new List<Heartbeat>();
            for (int i = 0; i < 2; i++) list.Add(new T());

            showAnimation.animation.SetHeartbeat(list[0]);
            hideAnimation.animation.SetHeartbeat(list[1]);

            return list;
        }

        private static void ResetAnimation(FloatAnimation animation)
        {
            var reaction = animation.animation;

            reaction.Reset();
            reaction.enabled = true;
            reaction.fromReferenceValue = ReferenceValue.CustomValue;
            reaction.fromCustomValue = 0f;
            reaction.toReferenceValue = ReferenceValue.CustomValue;
            reaction.toCustomValue = 1f;
            reaction.settings.duration = 0.5f;
        }
    }
}
