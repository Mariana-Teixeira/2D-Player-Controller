using System;
using System.IO;
using UnityEngine;

namespace marianateixeira.PlatformerController
{
    [Serializable]
    public struct PlayerData
    {
        #region Variables
        [SerializeField] float maxSpeed;
        [SerializeField] float accelerationTime;
        [SerializeField] float decelerationTime;
        [SerializeField] bool variableJump;
        [SerializeField] float jumpHeight;
        [SerializeField] float jumpTime;
        [SerializeField] float fallTime;
        #endregion

        #region Getters for Inspector Variables
        public float MaxSpeed
        {
            get
            {
                return maxSpeed;
            }
        }
        public float AccelerationTime
        {
            get
            {
                return accelerationTime;
            }
        }
        public float DecelerationTime
        {
            get
            {
                return decelerationTime;
            }
        }
        public bool VariableJump
        {
            get
            {
                return variableJump;
            }
        }
        public float JumpHeight
        {
            get
            {
                return jumpHeight;
            }
        }
        public float JumpTime
        {
            get
            {
                return  jumpTime;
            }
        }
        public float FallTime
        {
            get
            {
                return fallTime;
            }
        }
        #endregion

        public float JumpGravity { get; private set; }
        public float FallGravity { get; private set; }
        public float InitialJumpVelocity { get; private set; }
        public bool ReadyToJump { get; set; }

        public float RunSpeed { get; set; }
        public float Acceleration { get; private set; }
        public float Deceleration { get; private set; }
        public float AccelerationDirection { get; set; }

        public void Initialize()
        {
            ReadyToJump = true;

            JumpGravity = (-2 * jumpHeight) / (jumpTime * jumpTime);
            FallGravity = (-2 * jumpHeight) / (fallTime * fallTime);

            InitialJumpVelocity = -JumpGravity * jumpTime;

            Acceleration = MaxSpeed / AccelerationTime;
            Deceleration = MaxSpeed / DecelerationTime;
        }

        public void UpdateVariables(float maxSpeed, float accelerationTime, float decelerationTime, bool variableJump, float jumpHeight, float jumpTime, float fallTime)
        {
            this.maxSpeed = maxSpeed;
            this.accelerationTime = accelerationTime;
            this.decelerationTime = decelerationTime;
            this.variableJump = variableJump;
            this.jumpHeight = jumpHeight;
            this.jumpTime = jumpTime;
            this.fallTime = fallTime;
        }

        public void LoadFile()
        {
            string filePath = Path.Combine(Application.persistentDataPath, "playerConfig.json");

            if (File.Exists(filePath))
            {
                // Values from File
                string json = File.ReadAllText(filePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                UpdateVariables(data.MaxSpeed, data.AccelerationTime, data.DecelerationTime, data.VariableJump, data.JumpHeight, data.JumpTime, data.FallTime);
            }
        }

        public void SaveFile()
        {
            SaveData data = new SaveData()
            {
                MaxSpeed = maxSpeed,
                AccelerationTime = accelerationTime,
                DecelerationTime = decelerationTime,
                VariableJump = variableJump,
                JumpHeight = jumpHeight,
                JumpTime = jumpTime,
                FallTime = fallTime,
            };

            string json = JsonUtility.ToJson(data);
            string filePath = Path.Combine(Application.persistentDataPath, "playerConfig.json");
            File.WriteAllText(filePath, json);
        }

        private class SaveData
        {
            public float MaxSpeed;
            public float AccelerationTime;
            public float DecelerationTime;
            public bool VariableJump;
            public float JumpHeight;
            public float JumpTime;
            public float FallTime;
        }
    }
}