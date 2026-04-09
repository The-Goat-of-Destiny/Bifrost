using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Global Data", menuName = "Content/GlobalData", order = 0)]
public class GlobalData : ScriptableObject
{
    public List<Class> Classes = new();
    public List<Ancestry> Ancestries = new();
    public List<Background> Backgrounds = new();
    public List<Deity> Deities = new();
    public List<Domain> Domains = new();
    public List<Heritage> Heritages = new();

    public List<CharacterOption> GetOptions()
    {
        // Filtering not complete
        List<CharacterOption> Options = new();
        Options.AddRange(Classes);
        Options.AddRange(Ancestries);
        Options.AddRange(Backgrounds);
        Options.AddRange(Deities);
        Options.AddRange(Domains);
        Options.AddRange(Heritages);
        return Options;
    }

    private void OnValidate()
    {
        Game.Data = this;
    }
}
