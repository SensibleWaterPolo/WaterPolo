using UnityEngine;
using UnityEngine.Playables;

public class RefereeAnimationController : MonoBehaviour
{
    private ERefereeAnim _currentAnimations;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private AnimationClip _watch;

    [SerializeField]
    private AnimationClip _walkUp;

    [SerializeField]
    private AnimationClip _walkDown;

    [SerializeField]
    private AnimationClip _upLeftArm;

    [SerializeField]
    private AnimationClip _upRightArm;

    [SerializeField]
    private AnimationClip _upFrontArms;

    private PlayableGraph _playableGraph;


    public void PlayAnimation(ERefereeAnim anim)
    {
        if (_currentAnimations == anim)
            return;

        switch (anim)
        {
            case ERefereeAnim.WalkUp:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _walkUp, out _playableGraph);
                    _currentAnimations = ERefereeAnim.WalkUp;
                }
                break;

            case ERefereeAnim.WalkDown:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _walkDown, out _playableGraph);
                    _currentAnimations = ERefereeAnim.WalkDown;
                }
                break;

            case ERefereeAnim.UpLeftArm:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _upLeftArm, out _playableGraph);
                    _currentAnimations = ERefereeAnim.UpLeftArm;
                }
                break;

            case ERefereeAnim.UpRightArm:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _upRightArm, out _playableGraph);
                    _currentAnimations = ERefereeAnim.UpRightArm;
                }
                break;

            case ERefereeAnim.UpFrontArms:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _upFrontArms, out _playableGraph);
                    _currentAnimations = ERefereeAnim.UpFrontArms;
                }
                break;

            case ERefereeAnim.Watch:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _watch, out _playableGraph);
                    _currentAnimations = ERefereeAnim.Watch;
                }
                break;
        }
    }

    private void OnDestroy()
    {
        _playableGraph.Destroy();
    }


    public enum ERefereeAnim
    {
        WalkUp,
        WalkDown,
        UpLeftArm,
        UpRightArm,
        UpFrontArms,
        Watch
    }
}