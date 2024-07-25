using System.Diagnostics;

namespace PassXYZ.Vault;

public class PxWindow : Window
{
    public PxWindow() : base() {}

    public PxWindow(Page page) : base(page) {}

    protected override void OnCreated() {
        Debug.WriteLine("PassXYZ.Vault.App: 1. OnCreated");
    }

    protected override void OnActivated() {
        Debug.WriteLine("PassXYZ.Vault.App: 2. OnActivated");
    }

        protected override void OnDeactivated() {
        Debug.WriteLine("PassXYZ.Vault.App: 3. OnDeactivated");
    }

        protected override void OnStopped() {
        Debug.WriteLine("PassXYZ.Vault.App: 4. OnStopped");
    }
        protected override void OnResumed() {
        Debug.WriteLine("PassXYZ.Vault.App: 5. OnResumed");
    }
    
        protected override void OnDestroying() {
        Debug.WriteLine("PassXYZ.Vault.App: 6. OnDestroying");
    }
}
