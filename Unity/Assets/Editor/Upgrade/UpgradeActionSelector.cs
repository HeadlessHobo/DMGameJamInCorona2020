using System;
using Common.UpgradeSystem;
using UnityEditor;

namespace Editor.Upgrade
{
    [CustomPropertyDrawer(typeof(UpgradeActionSelector))]
    public class UpgradeActionSelectorPropertyEditor : TypeSelectorDrawer
    {
        protected override string IndexPropertyName => "_upgradeActionIndex";

        protected override string NamesPropertyName => "_upgradeActionNames";

        protected override Type SelectorType => typeof(IUpgradeAction);
    }
}