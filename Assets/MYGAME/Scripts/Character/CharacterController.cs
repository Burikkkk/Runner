using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;


public class CharacterController : MonoBehaviour
{

	public GameObject blobShadow;
	public float laneChangeTime = 0.05f;
    private Animator animator;
	public float trackSpeed=10.0f;
	public bool isJumping { get { return m_Jumping; } }
	public bool isSliding { get { return m_Sliding; } }
	
	public AudioClip deathSound;

	protected bool m_IsRunning;
    protected bool m_Jumping;
	protected bool m_Sliding;


    protected int m_CurrentLane = k_StartingLane;
    protected Vector3 m_TargetPosition = Vector3.zero;


    protected const int k_StartingLane = 1;
    
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
	    m_IsRunning = true;
		animator= GetComponent<Animator>();
    }
    
    private void Start()
    {
		StartRunning();
		StartSpeedIncrease();
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
		if (m_CurrentLane < 0)
		{
			m_CurrentLane = 0;
		}
		else if (m_CurrentLane >= generator.lanesX.Length)
		{
			m_CurrentLane = generator.lanesX.Length - 1;
		}

		m_TargetPosition = transform.position;
		m_TargetPosition.x = generator.lanesX[m_CurrentLane];
	}

    protected void Update ()
    {
		if (m_IsRunning)
		{
			var lerpPosition = Vector3.Lerp(transform.position, m_TargetPosition, laneChangeTime);
			var deltaX = lerpPosition.x - transform.position.x;
			transform.Translate(deltaX, 0.0f, trackSpeed*Time.deltaTime);	
		}
		
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(-1);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(1);
        }
        else if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
		else if (Input.GetKeyDown(KeyCode.S))
		{
			if(!m_Sliding)
				Slide();
		}
	}

    public void Die()
    {
	    StopMoving();
	    StopJumping();
	    StopSliding();
	    animator.SetBool(s_DeadHash, true);
	    LevelManager.instance.audio.PlayOneShot(deathSound);
    }

    public void EndDeathAnim()
    {
	    LevelManager.instance.EndGame();
    }

    public void Jump()
    {
	    if (!m_IsRunning)
		    return;
	    
        if (!m_Jumping)
        {
            animator.SetFloat(s_JumpingSpeedHash, 1.0f);
            animator.SetBool(s_JumpingHash, true);
			m_Jumping = true;
        }
    }

 public void StopJumping()
 {
     if (m_Jumping)
     {
         animator.SetBool(s_JumpingHash, false);
         m_Jumping = false;
     }
 }

	public void Slide()
	{
		if (!m_Sliding)
		{

		    if (m_Jumping)
		        StopJumping();

			animator.SetFloat(s_JumpingSpeedHash, 1.0f);
			animator.SetBool(s_SlidingHash, true);
			m_Sliding = true;
		}
	}

	public void StopSliding()
	{
		if (m_Sliding)
		{
			animator.SetBool(s_SlidingHash, false);
			m_Sliding = false;
		}
	}
	
	public void StartSpeedIncrease()
	{
		StartCoroutine(IncreaseTeam());
	}

	private IEnumerator IncreaseTeam()
	{
		trackSpeed = 10.0f;
		yield return new WaitForSeconds(10.0f);
		trackSpeed = 13.0f;
		yield return new WaitForSeconds(10.0f);
		trackSpeed = 15.0f;
		yield return new WaitForSeconds(10.0f);
		trackSpeed = 20.0f;
		yield return new WaitForSeconds(10.0f);
	}
}
