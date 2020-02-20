using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class AnimatorHandler : MonoBehaviour {

    public SkeletonAnimation skeletonAnimator;

    [SerializeField]
    public List<AnimationJuguete> animList = new List<AnimationJuguete>();

    public PlayerState actualState;

    public Player player;

    private void Awake()
    {
        skeletonAnimator = GetComponent<SkeletonAnimation>();
        actualState = PlayerState.Idle;
    }
    void Start()
    {
        skeletonAnimator.AnimationState.Complete += AnimationCompleteHandler;
    }
    void AnimationCompleteHandler(TrackEntry track)
    {
        // Reiniciamos la animacion que termina, solo si esta completada y si esta en el track 1( armas)
        if (track.TrackIndex == 1 && track.IsComplete)
        {
            skeletonAnimator.AnimationState.SetEmptyAnimation(1, 0.5f);
            if (track.animation.name == "melee")
            {
                player.ArmAttack();
            }
            if (track.animation.name == "attack")
            {
                SetAnimation(1, "attack_empty", false);
            }
            // state.ClearTrack(1);
        }

    }
    public void AgarrasteBala()
    {
        SetAnimation(1, "attack_loaded", false);
    }
    public void ChangeSkin(string skin)
    {
        skeletonAnimator.skeleton.SetSkin(skin);
        skeletonAnimator.skeleton.SetToSetupPose(); 
       // skeletonAnimator.AnimationState.ClearTrack(0);
    }

    TrackEntry SetAnimation(string animation, bool loop)
    {
        return skeletonAnimator.AnimationState.SetAnimation(0, animation, loop);
    }

    TrackEntry SetAnimation(int layer, string animation, bool loop)
    {
        return skeletonAnimator.AnimationState.SetAnimation(layer, animation, loop);

    }
    public string GetAnimation(PlayerState state)
    {

        foreach (AnimationJuguete animAux in animList)
        {
            if (animAux.state == state)
                return animAux.animation.Animation.Name;
        }
        Debug.LogError("ERRROR: Animation not found !");
        return "ERRROR: Animation not found !";
    }

    public void Attack()
    {
        SetAnimation(1,GetAnimation(PlayerState.Attack), false);
    }
    public void Melee()
    {
        SetAnimation(1, GetAnimation(PlayerState.Melee), false);
    }
    public void PlayerWalk()
    {
        ChangeState(PlayerState.Walk);
    }

    public void PlayerRun()
    {
        ChangeState(PlayerState.Run);
    }
    public void PlayerIdle()
    {
        ChangeState(PlayerState.Idle);
    }

    void ChangeState(PlayerState newState)
    {
        if (newState != actualState)
        {
            //Debug.Log("Enter: " + newState);
            actualState = newState;

            //TODO: PASAR LOGICA DE ANIMACION AL CONTROLADOR
            string anim = GetAnimation(newState);

            SetAnimation(anim, true);
        }
    }

    // Use this for initialization
    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    public class AnimationJuguete
    {
        public AnimationReferenceAsset animation;

        public PlayerState state; 
    }
}
