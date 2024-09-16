using UnityEngine.InputSystem;
using TechnicalTest.System.InputSystem;
using UnityEngine;

namespace TechnicalTest.System.PlayerSystem.Controller
{
	/// <summary>
	/// Class that controls the basic actions of the player.
	/// </summary>
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		// cinemachine
		private float cinemachineTargetPitch;

		// player
		private float speed;
		private float rotationVelocity;
		private float verticalVelocity;
		private float terminalVelocity = 53.0f;

		// timeout deltatime
		private float jumpTimeoutDelta;
		private float fallTimeoutDelta;


		private PlayerInput playerInput;
		private CharacterController controller;
		private InputController input;
		private GameObject mainCamera;
		private Animator playerAnimator;

		private const float threshold = 0.01f;

		private bool IsCurrentDeviceMouse
		{
			get
			{
				return playerInput.currentControlScheme == "Keyboard";
			}
		}

		private void Awake()
		{
			if (mainCamera == null)
			{
				mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			controller = GetComponent<CharacterController>();
			input = GetComponent<InputController>();
			playerInput = GetComponent<PlayerInput>();
			playerAnimator = GetComponent<Animator>();

			jumpTimeoutDelta = JumpTimeout;
			fallTimeoutDelta = FallTimeout;
		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			Move();
		}

		private void LateUpdate()
		{
			CameraRotation();
		}

		private void GroundedCheck()
		{
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			if (input.look.sqrMagnitude >= threshold)
			{
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

				cinemachineTargetPitch += input.look.y * RotationSpeed * deltaTimeMultiplier;
				rotationVelocity = input.look.x * RotationSpeed * deltaTimeMultiplier;

				cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, BottomClamp, TopClamp);

				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

				transform.Rotate(Vector3.up * rotationVelocity);
			}
		}

		private void Move()
		{
			float targetSpeed = input.sprint ? SprintSpeed : MoveSpeed;

			if (input.move == Vector2.zero) targetSpeed = 0.0f;

			float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;

			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				speed = Mathf.Round(speed * 1000f) / 1000f;
			}
			else
			{
				speed = targetSpeed;
			}

			Vector3 inputDirection = new Vector3(input.move.x, 0.0f, input.move.y).normalized;

			if (input.move != Vector2.zero)
			{
				inputDirection = transform.right * input.move.x + transform.forward * input.move.y;
			}

			playerAnimator.SetFloat("Speed", Mathf.Abs(input.move.x) + Mathf.Abs(input.move.y));
			controller.Move(inputDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				fallTimeoutDelta = FallTimeout;

				if (verticalVelocity < 0.0f)
				{
					verticalVelocity = -2f;
				}

				if (input.jump && jumpTimeoutDelta <= 0.0f)
				{
					verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}

				if (jumpTimeoutDelta >= 0.0f)
				{
					jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				jumpTimeoutDelta = JumpTimeout;

				if (fallTimeoutDelta >= 0.0f)
				{
					fallTimeoutDelta -= Time.deltaTime;
				}

				input.jump = false;
			}

			if (verticalVelocity < terminalVelocity)
			{
				verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}
