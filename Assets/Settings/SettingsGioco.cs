using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsGioco", menuName = "Installers/SettingsGioco")]
public class SettingsGioco : ScriptableObjectInstaller<SettingsGioco>
{
    public int score;

    public override void InstallBindings()
    {

    }
}