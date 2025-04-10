using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;


public class CharacterController : MonoBehaviour
{

	public GameObject blobShadow;
	public float laneChangeSpeed = 1.0f;
    private Animator animator;
	public float trackSpeed=10.0f;
	public int coins { get { return m_Coins; } set { m_Coins = value; } }
	public bool isJumping { get { return m_Jumping; } }
	public bool isSliding { get { return m_Sliding; } }

	[Header("Controls")]
	public float jumpLength = 2.0f;     // Distance jumped
	public float jumpHeight = 1.2f;
	public float slideLength = 2.0f;

    protected int m_Coins;

	protected bool m_IsRunning;
	
    protected float m_JumpStart;
    protected bool m_Jumping;

	protected bool m_Sliding;
	protected float m_SlideStart;


    protected int m_CurrentLane = k_StartingLane;
    protected Vector3 m_TargetPosition = Vector3.zero;


    protected const int k_StartingLane = 1;
    protected const float k_GroundingSpeed = 80f;

    protected const float k_ShadowGroundOffset = 0.01f;
    protected const float k_TrackSpeedToJumpAnimSpeedRatio = 0.6f;
    protected const float k_TrackSpeedToSlideAnimSpeedRatio = 0.9f;

    static int s_DeadHash = Animator.StringToHash("Dead");
    static int s_RunStartHash = Animator.StringToHash("runStart");
    static int s_MovingHash = Animator.StringToHash("Moving");
    static int s_JumpingHash = Animator.StringToHash("Jumping");
    static int s_JumpingSpeedHash = Animator.StringToHash("JumpSpeed");
    static int s_SlidingHash = Animator.StringToHash("Sliding");

	public TrackGenerator generator;
    protected void Awake ()
    {
        m_Sliding = false;
        m_SlideStart = 0.0f;
	    m_IsRunning = true;
		animator= GetComponent<Animator>();
    }

    

    public void Init()
    {
        transform.position = generator.start.position;
		m_TargetPosition = Vector3.zero;
		m_CurrentLane = k_StartingLane;
	
    }

    private void Start()
    {
		StartRunning();
    }


    private void StartRunning()
    {   
	    m_IsRunning = true;
        if (animator)
        {
        animator.Play(s_RunStartHash);
        animator.SetBool(s_MovingHash, true);
        }
    }



    private void StopMoving()
    {
	    m_IsRunning = false;

        if (animator)
        {
            animator.SetBool(s_MovingHash, false);
        }
    }

	private void ChangeLane(int i) { 
		m_CurrentLane+=i;
		var posX = generator.lanesX[m_CurrentLane];
	}

    protected void Update ()
    {
		transform.Translate(0.0f, 0.0f, trackSpeed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(-1);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(1);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            //Jump();
        }
		else if (Input.GetKeyDown(KeyCode.S))
		{
			//if(!m_Sliding)
				//Slide();
		}

        Vector3 verticalTargetPosition = m_TargetPosition;

		//if (m_Sliding)
		//{
		//	float correctSlideLength = slideLength * (1.0f + trackManager.speedRatio);
		//	float ratio = (trackManager.worldDistance - m_SlideStart) / correctSlideLength;
		//	if (ratio >= 1.0f)
		//	{
                
		//		StopSliding();
		//	}
		//}

   //     if(m_Jumping)
   //     {
			//if (trackManager.isMoving)
			//{
   //             // Same as with the sliding, we want a fixed jump LENGTH not fixed jump TIME. Also, just as with sliding,
   //             // we slightly modify length with speed to make it more playable.
			//	float correctJumpLength = jumpLength * (1.0f + trackManager.speedRatio);
			//	float ratio = (trackManager.worldDistance - m_JumpStart) / correctJumpLength;
			//	if (ratio >= 1.0f)
			//	{
			//		m_Jumping = false;
			//		character.animator.SetBool(s_JumpingHash, false);
			//	}
			//	else
			//	{
			//		verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
			//	}
			//}
			//else if(!AudioListener.pause)//use AudioListener.pause as it is an easily accessible singleton & it is set when the app is in pause too
			//{
			//    verticalTargetPosition.y = Mathf.MoveTowards (verticalTargetPosition.y, 0, k_GroundingSpeed * Time.deltaTime);
			//	if (Mathf.Approximately(verticalTargetPosition.y, 0f))
			//	{
			//		character.animator.SetBool(s_JumpingHash, false);
			//		m_Jumping = false;
			//	}
			//}
   //     }

        //characterCollider.transform.localPosition = Vector3.MoveTowards(characterCollider.transform.localPosition, verticalTargetPosition, laneChangeSpeed * Time.deltaTime);

        //// Put blob shadow under the character.
        //RaycastHit hit;
        //if(Physics.Raycast(characterCollider.transform.position + Vector3.up, Vector3.down, out hit, k_ShadowRaycastDistance, m_ObstacleLayer))
        //{
        //    blobShadow.transform.position = hit.point + Vector3.up * k_ShadowGroundOffset;
        //}
        //else
        //{
        //    Vector3 shadowPosition = characterCollider.transform.position;
        //    shadowPosition.y = k_ShadowGroundOffset;
        //    blobShadow.transform.position = shadowPosition;
        //}
	}

 //   public void Jump()
 //   {
	//    if (!m_IsRunning)
	//	    return;
	    
 //       if (!m_Jumping)
 //       {
	//		if (m_Sliding)
	//			StopSliding();

	//		float correctJumpLength = jumpLength * (1.0f + trackManager.speedRatio);
	//		m_JumpStart = trackManager.worldDistance;
 //           float animSpeed = k_TrackSpeedToJumpAnimSpeedRatio * (trackManager.speed / correctJumpLength);

 //           character.animator.SetFloat(s_JumpingSpeedHash, animSpeed);
 //           character.animator.SetBool(s_JumpingHash, true);
	//		m_Audio.PlayOneShot(character.jumpSound);
	//		m_Jumping = true;
 //       }
 //   }

 //   public void StopJumping()
 //   {
 //       if (m_Jumping)
 //       {
 //           character.animator.SetBool(s_JumpingHash, false);
 //           m_Jumping = false;
 //       }
 //   }

	//public void Slide()
	//{
	//	if (!m_IsRunning)
	//		return;
		
	//	if (!m_Sliding)
	//	{

	//	    if (m_Jumping)
	//	        StopJumping();

 //           float correctSlideLength = slideLength * (1.0f + trackManager.speedRatio); 
	//		m_SlideStart = trackManager.worldDistance;
 //           float animSpeed = k_TrackSpeedToJumpAnimSpeedRatio * (trackManager.speed / correctSlideLength);

	//		character.animator.SetFloat(s_JumpingSpeedHash, animSpeed);
	//		character.animator.SetBool(s_SlidingHash, true);
	//		m_Audio.PlayOneShot(slideSound);
	//		m_Sliding = true;

	//		characterCollider.Slide(true);
	//	}
	//}

	//public void StopSliding()
	//{
	//	if (m_Sliding)
	//	{
	//		animator.SetBool(s_SlidingHash, false);
	//		m_Sliding = false;

	//		characterCollider.Slide(false);
	//	}
	//}


    
}
