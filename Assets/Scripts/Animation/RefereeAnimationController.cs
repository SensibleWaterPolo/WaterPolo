using UnityEngine;
using UnityEngine.Playables;

public class RefereeAnimationController : MonoBehaviour
{
    private string _currentClipName;

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

    private void Start()
    {

    }

    public void PlayAnimation(ERefereeAnim anim)
    {
        if (_currentClipName == ConvertEnumToString(anim))
            return;

        switch (anim)
        {
            case ERefereeAnim.WalkUp:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _walkUp, out _playableGraph);
                    _currentClipName = "WalkUp";
                }
                break;

            case ERefereeAnim.WalkDown:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _walkDown, out _playableGraph);
                    _currentClipName = "WalkDown";
                }
                break;

            case ERefereeAnim.UpLeftArm:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _upLeftArm, out _playableGraph);
                    _currentClipName = "UpLeftArm";
                }
                break;

            case ERefereeAnim.UpRightArm:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _upRightArm, out _playableGraph);
                    _currentClipName = "UpRightArm";
                }
                break;

            case ERefereeAnim.UpFrontArms:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _upFrontArms, out _playableGraph);
                    _currentClipName = "UpFrontArms";
                }
                break;

            case ERefereeAnim.Watch:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _watch, out _playableGraph);
                    _currentClipName = "Watch";
                }
                break;
        }
    }

    private void OnDestroy()
    {
        _playableGraph.Destroy();
    }

    private string ConvertEnumToString(ERefereeAnim anim)
    {
        string result = "";
        switch (anim)
        {
            case ERefereeAnim.Watch:
                result = "Watch";
                break;

            case ERefereeAnim.UpLeftArm:
                result = "UpLeftArm";
                break;

            case ERefereeAnim.UpRightArm:
                result = "UpRightArm";
                break;

            case ERefereeAnim.UpFrontArms:
                result = "UpFrontArms";
                break;

            case ERefereeAnim.WalkUp:
                result = "WalkUp";
                break;

            case ERefereeAnim.WalkDown:
                result = "WalkDown";
                break;
        }
        return result;
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