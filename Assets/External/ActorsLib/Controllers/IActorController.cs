namespace ActorsLib.Controllers
{
    using UnityEngine;

    /// <summary>
    /// An interface for an abstracted controller class using <see cref="ActorAction"/>s.
    /// </summary>
    public interface IActorController
    {
        /// <summary>
        /// Activates an action, giving it a value of <see cref="Vector2.one"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        void Activate(string actionName);

        /// <summary>
        /// Actives an action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="value">The value to activate with.</param>
        void Activate(string actionName, Vector2 value);

        /// <summary>
        /// Deactives an action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        void Deactivate(string actionName);

        /// <summary>
        /// Determines if an action is active this frame.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>True if the action is active this frame.</returns>
        bool IsActive(string actionName);

        /// <summary>
        /// Determines if an action was active in the last frame.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>True if the action was active last frame.</returns>
        bool WasActive(string actionName);

        /// <summary>
        /// Is active this frame but was not active last frame.
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        bool IsTriggered(string actionName);

        /// <summary>
        /// Returns true if the action has been active for the given duration.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="duration">A duration in seconds.</param>
        /// <returns>True if the action has been active for the given duration.</returns>
        bool IsHeldFor(string actionName, float duration);

        /// <summary>
        /// Gets the x value of an action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The x value component of the action value.</returns>
        float GetFloat(string actionName);

        /// <summary>
        /// Gets the non-normalized value of an action. If you are looking for
        /// the normalized value use <see cref="GetDirection"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The value of an action.</returns>
        Vector2 GetVector(string actionName);

        /// <summary>
        /// Gets the normalized value of an action. If you are looking for
        /// the non-normalized value use <see cref="GetVector"/>.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The normalized value of an action.</returns>
        Vector2 GetDirection(string actionName);

        /// <summary>
        /// Gets the value an action clamped to a circle.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The value of an action clamped to a circle.</returns>
        Vector2 GetRadial(string actionName);

        /// <summary>
        /// Gets the angle of an action's value in degrees.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The angle of an action's value in degrees.</returns>
        float GetAngle(string actionName);

        /// <summary>
        /// Clears an action's hold time and value.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        void Flush(string actionName);

        /// <summary>
        /// Clears all actions' hold time and value.
        /// </summary>
        void Flush();

        /// <summary>
        /// Gets the icon assigned to an action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The icon assigned to an action.</returns>
        Sprite GetIcon(string actionName);

        /// <summary>
        /// Updates the state of the actions.
        /// </summary>
        void Update();

        /// <summary>
        /// Updates the state of the actions.
        /// </summary>
        void LateUpdate();
    }
}
