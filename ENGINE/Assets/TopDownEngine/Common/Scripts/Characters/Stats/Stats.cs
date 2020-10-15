using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

    /// <summary>
    /// This class manages the stats of an object
	/// </summary>
    public class Stats : MonoBehaviour
    {
        [Header("Health")]

        [Tooltip("The base maximum health of the character")]
        public int baseMaximumHealth = 10;

        [Tooltip("The initial health of the character")]
        public int initialHealth = 10;


        [Header("Damage Reduction")]

        [Tooltip("The base damage reduction percentaje of the character")]
        public float baseDamageReduction;


        [Header("Movement Speed")]

        [Tooltip("The characters base movement speed")]
        public float baseMovementSpeed = 5f;

        [Tooltip("Base movement speed multiplier")]
        public float movementSpeedMultiplier = 1f;

        
        

        protected EffectManager _effectManager;

        /// <summary>
        /// On awake we initialize our stats
        /// </summary>
        protected virtual void Awake()
        {
            Initialization();
        }

        /// <summary>
        /// We assign important components
        /// </summary>
        protected virtual void Initialization()
        {

            //We assign the Effect Manager
            if(_effectManager == null)
            {
                if(GetComponent<EffectManager>() != null)
                {
                    _effectManager = GetComponent<EffectManager>();
                }
            }
            
        }


        /// <summary>
        /// This property returns the characters movement speed after all modifiers are applied (Read Only)
        /// </summary>
        public float MovementSpeed
        {
            get
            {
                //Calculate the total base movement speed
                float totalBaseMovementSpeed = baseMovementSpeed + (_effectManager != null ? _effectManager.MovementSpeed : 0);

                //Calculate the total movement speed multiplier
                float totalMovementSpeedMultiplier = movementSpeedMultiplier * (_effectManager != null ? _effectManager.MovementSpeedMultiplier : 1);

                //calculate the slow
                float totalSlow = _effectManager ? _effectManager.Slow : 0;

                ///we calculate the final movement speed
                return (totalBaseMovementSpeed * totalMovementSpeedMultiplier) * ((100 - totalSlow) / 100f);
            }
        }

        /// <summary>
        /// This property return the characters maximum health after all modifiers are applied
        /// </summary>
        public int MaximumHealth
        {
            get
            {
                //Calculate the total base maximum health
                int totalBaseMaxHealth = baseMaximumHealth + (_effectManager != null ? _effectManager.MaxHealth : 0);

                //calculate the total maximum health multiplier
                float totalMaximumHealthMultiplier = (_effectManager != null ? _effectManager.MaxHealthMultiplier : 1);

                //we return the final maximum health
                return (int)(totalBaseMaxHealth * totalMaximumHealthMultiplier);
            }
        }

        /// <summary>
        /// This property returns the characters damage reduction percentaje after all modifiers are applied
        /// </summary>
        public float DamageReduction
        {
            get
            {
                return Mathf.Clamp(baseDamageReduction + (_effectManager != null ? _effectManager.DamageReduction : 0), 0, 100);
            }
        }
    }


