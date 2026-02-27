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
            if (ending == Ending.Placeholder)
            {
                SetDialogue(null);
                return;
            }

            ending = Utils.Filter(ending, endingOverrides);
            SetDialogue(Resources.Load<TextAsset>($"{ending}/{resourceName}_{ending.ToString().ToLowerInvariant()}"));
        }
    }
}
