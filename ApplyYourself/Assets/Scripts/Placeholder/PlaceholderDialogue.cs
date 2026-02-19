using UnityEngine;

namespace ApplyYourself
{
    public class PlaceholderDialogue : BasePlaceholder
    {
        private void SetDialogue(TextAsset dialogue)
        {
            GetComponent<Dialogue>().SetDialogue(dialogue);
        }

        public override void SetAs(Ending ending)
        {
            ending = Utils.Filter(ending, endingOverrides);
            SetDialogue(Resources.Load<TextAsset>($"{ending}/{resourceName}"));
        }
    }
}
