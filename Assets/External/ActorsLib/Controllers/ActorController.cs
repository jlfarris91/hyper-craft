namespace ActorsLib.Controllers
{
    using System;
    using System.Collections.Generic;
    using CommonLib;
    using UnityEngine;

    [Serializable]
    public class ActorController : MonoBehaviour, IActorController
    {
        public bool ShowValues = false;

        private readonly Dictionary<string, ActorAction> actions = new Dictionary<string, ActorAction>();

        public virtual IDictionary<string, ActorAction> Actions => this.actions;

        public void Activate(string actionName)
        {
            this.Activate(actionName, Vector2.one);
        }

        public void Activate(string actionName, Vector2 value)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            if (action != null)
            {
                action.Value = value;
            }
        }

        public void Activate(string actionName, ActorAction other)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            action?.CopyFrom(other);
        }

        public void Deactivate(string actionName)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            if (action != null)
            {
                action.Value = Vector2.zero;
            }
        }

        public bool IsActive(string actionName)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            return action != null && action.IsActive;
        }

        public bool WasActive(string actionName)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            return action != null && action.WasActive;
        }

        public bool IsTriggered(string actionName)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            return action != null && action.IsActive && !action.WasActive;
        }

        public bool IsReleased(string actionName)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            return action != null && !action.IsActive && action.WasActive;
        }

        public bool IsHeldFor(string actionName, float duration)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            return action != null && action.HeldTime > duration;
        }

        public float GetFloat(string actionName)
        {
            return this.GetVector(actionName).magnitude;
        }

        public Vector2 GetVector(string actionName)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            return action?.Value ?? Vector2.zero;
        }

        public Vector2 GetDirection(string actionName)
        {
            return this.GetVector(actionName).normalized;
        }

        public Vector2 GetRadial(string actionName)
        {
            return this.GetVector(actionName).AsRadial();
        }

        public float GetAngle(string actionName)
        {
            return MathEx.GetAngle(this.GetVector(actionName).normalized);
        }

        public void Flush(string actionName)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            action.Flush();
        }

        public void Flush()
        {
            this.Actions.Values.Foreach(action => action.Flush());
        }

        public Sprite GetIcon(string actionName)
        {
            ActorAction action = this.GetOrCreateAction(actionName);
            return action?.Icon;
        }

        protected ActorAction GetOrCreateAction(string actionName)
        {
            if (!this.actions.ContainsKey(actionName))
            {
                this.actions.Add(actionName, new ActorAction(actionName));
            }
            return this.actions[actionName];
        }

        public void Update()
        {
            foreach (ActorAction action in this.actions.Values)
            {
                if (action.IsActive)
                {
                    action.HeldTime += Time.deltaTime;
                }
                else
                {
                    action.HeldTime = 0.0f;
                }
            }
        }

        public void LateUpdate()
        {
            foreach (ActorAction action in this.actions.Values)
            {
                action.LastValue = action.Value;
            }
        }

        private void OnGUI()
        {
            if (this.ShowValues)
            {
                GUILayout.BeginVertical();

                foreach (var action in this.actions)
                {
                    GUILayout.Label($"{action.Key}: {action.Value.LastValue}");
                }

                GUILayout.EndVertical();
            }
        }
    }
}