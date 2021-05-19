using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private AnimationClip _possesso;

    [SerializeField]
    private AnimationClip _bicicletta;

    [SerializeField]
    private AnimationClip _nuotoDorso;

    [SerializeField]
    private AnimationClip _nuotoConPalla;

    [SerializeField]
    private AnimationClip _tiro;

    [SerializeField]
    private AnimationClip _nuotoStile;

    [SerializeField]
    private AnimationClip _tiroColonnello;

    [SerializeField]
    private AnimationClip _tiroRovesciata;

    [SerializeField]
    private AnimationClip _tiroSciarpa;

    [SerializeField]
    private AnimationClip _stunnato;

    [SerializeField]
    private AnimationClip _difesaBraccio;

    private ETypeAnimation _currentAnimation;

    private Animator _animator;
    private PlayableGraph _playableGraph;

    [SerializeField]
    private GameObject _effect;

    private void Start()
    {
        _animator = transform.GetComponent<Animator>();

    }

    public void PlayAnimation(ETypeAnimation animation)
    {
        if (_currentAnimation == animation)
            return;

        _effect.SetActive(false);

        switch (animation)
        {
            case ETypeAnimation.Bicicletta:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _bicicletta, out _playableGraph);
                    _currentAnimation = ETypeAnimation.Bicicletta;
                }
                break;

            case ETypeAnimation.NuotoStile:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _nuotoStile, out _playableGraph);
                    _currentAnimation = ETypeAnimation.NuotoStile;
                    _effect.SetActive(true);

                }
                break;

            case ETypeAnimation.NuotoDorso:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _nuotoDorso, out _playableGraph);
                    _currentAnimation = ETypeAnimation.NuotoDorso;
                    _effect.SetActive(true);
                }
                break;

            case ETypeAnimation.NuotoConPalla:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _nuotoConPalla, out _playableGraph);
                    _currentAnimation = ETypeAnimation.NuotoConPalla;
                    _effect.SetActive(true);
                }
                break;

            case ETypeAnimation.Possesso:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _possesso, out _playableGraph);
                    _currentAnimation = ETypeAnimation.Possesso;
                }
                break;

            case ETypeAnimation.Tiro:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _tiro, out _playableGraph);
                    _currentAnimation = ETypeAnimation.Tiro;
                }
                break;

            case ETypeAnimation.TiroColonnello:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _tiroColonnello, out _playableGraph);
                    _currentAnimation = ETypeAnimation.TiroColonnello;
                }
                break;

            case ETypeAnimation.TiroRovesciata:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _tiroRovesciata, out _playableGraph);
                    _currentAnimation = ETypeAnimation.TiroRovesciata;
                }
                break;

            case ETypeAnimation.TiroSciarpa:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _tiroSciarpa, out _playableGraph);
                    _currentAnimation = ETypeAnimation.TiroSciarpa;
                }
                break;

            case ETypeAnimation.Stunnato:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _stunnato, out _playableGraph);
                    _currentAnimation = ETypeAnimation.Stunnato;
                }
                break;

            case ETypeAnimation.DifesaBraccio:
                {
                    AnimationPlayableUtilities.PlayClip(_animator, _difesaBraccio, out _playableGraph);
                    _currentAnimation = ETypeAnimation.DifesaBraccio;
                }
                break;
        }
    }

    private void OnDestroy()
    {
        _playableGraph.Destroy();
    }

    public enum ETypeAnimation
    {
        None,
        Bicicletta,
        NuotoDorso,
        NuotoStile,
        NuotoConPalla,
        Possesso,
        Tiro,
        TiroColonnello,
        TiroRovesciata,
        TiroSciarpa,
        Stunnato,
        DifesaBraccio
    }
}