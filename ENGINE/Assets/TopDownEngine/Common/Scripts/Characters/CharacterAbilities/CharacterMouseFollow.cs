using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MoreMountains.TopDownEngine // you might want to use your own namespace here
{
    /// <summary>
    /// Add this ability to a character to follow the mouse position
    /// </summary>
    [RequireComponent(typeof(CharacterPathfinder3D))]
    [RequireComponent(typeof(Stats))]
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Mouse Follow")]
    public class CharacterMouseFollow : CharacterAbility
    {

        [Header("Target Options")]

        [Tooltip("The distance at which the characters stops from its target")]
        public float enemyStopDistance = 5f;

        [Tooltip("Layers we can hit as target")]
        public LayerMask layers;

        protected const string _speedAnimationParameterName = "Speed";
        protected const string _walkingAnimationParameterName = "Walking";
        protected const string _idleAnimationParameterName = "Idle";
        protected Stats _stats;
        protected CharacterPathfinder3D _pathfinder3D;



        /// <summary>
        /// Here you should initialize our parameters
        /// </summary>
        protected override void Initialization()
        {
            base.Initialization();
            

            //we assign the stats
            _stats = GetComponent<Stats>();

            //we assign the CharacterPathfinder3D
            _pathfinder3D = GetComponent<CharacterPathfinder3D>();


        }

        /// <summary>
        /// Every frame, we check if we're crouched and if we still should be
        /// </summary>
        public override void ProcessAbility()
        {
            base.ProcessAbility();


        }

        /// <summary>
        /// Called at the start of the ability's cycle, this is where you'll check for input
        /// </summary>
        protected override void HandleInput()
        {
            // here as an example we check if we're pressing down
            // on our main stick/direction pad/keyboard
            //if (_inputManager.PrimaryMovement.y < -_inputManager.Threshold.y)
            //{
            //    DoSomething();
            //}

            print("Entra en HandleInput");

            //input provisional
            if(Input.GetMouseButtonDown(0))
            {
                SearchTarget();
            }

        }


        protected void SearchTarget()
        {
            //cámara provisional
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
            {
                //if the hitted target is a character
                if(hit.transform.gameObject.GetComponent<Character>())
                {
                    Character otherCharacter = hit.transform.gameObject.GetComponent<Character>();

                    _pathfinder3D.Target = otherCharacter.transform;
                    _pathfinder3D.DistanceToWaypointThreshold = enemyStopDistance;
                }
                else
                {
                    _pathfinder3D.Target = hit.transform;
                    _pathfinder3D.DistanceToWaypointThreshold = 0f;
                }

            }
        }

        /// <summary>
        /// If we're pressing down, we check for a few conditions to see if we can perform our action
        /// </summary>
        protected virtual void DoSomething()
        {
            // if the ability is not permitted
            if (!AbilityPermitted
                // or if we're not in our normal stance
                || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
                // or if we're grounded
                || (!_controller.Grounded))
            {
                // we do nothing and exit
                return;
            }

            // if we're still here, we display a text log in the console
            MMDebug.DebugLogTime("We're doing something yay!");
        }

        /// <summary>
        /// Adds required animator parameters to the animator parameters list if they exist
        /// </summary>
        protected override void InitializeAnimatorParameters()
        {
            //RegisterAnimatorParameter(_yourAbilityAnimationParameterName, AnimatorControllerParameterType.Bool, out _yourAbilityAnimationParameter);
        }

        /// <summary>
        /// At the end of the ability's cycle,
        /// we send our current crouching and crawling states to the animator
        /// </summary>
        public override void UpdateAnimator()
        {

            //bool myCondition = true;
            //MMAnimatorExtensions.UpdateAnimatorBool(_animator, _yourAbilityAnimationParameter, myCondition, _character._animatorParameters);
        }
    }
}

