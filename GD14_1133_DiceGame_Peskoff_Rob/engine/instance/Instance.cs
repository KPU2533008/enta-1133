using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.@interface;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.instance {
	public abstract class Instance : IDestroyable {

		private string _className = "Instance";
		private List<Instance> _children = new();
		private Instance? _parent = null;

		public Instance? Parent {
			get {
				return _parent;
			}
			set {
				if ( value == _parent )
					return;

				if ( value == this )
					return;

				if ( _parent != null ) {
					Instance? ancestor = _parent;
					while ( ancestor != null ) {
						// DescendantRemoving:Fire()
						ancestor = ancestor.Parent;
					}
				}

				if ( _parent != null ) {
					_parent._children.Remove(this);
				}

				if ( value != null ) {
					value._children.Add(this);
				}

				_parent = value;
			}
		}

		public bool IsA(Type target) {
			bool result = false;
			Type type = GetType();

			while ( type != typeof(Instance) ) {
				if ( type == target ) {
					result = true;
					break;
				}
				type = type.BaseType ?? typeof(Instance);
			}

			return result;
		}

		public Instance? FindFirstAncestorWhichIsA(Type type) {
			Instance? parent = this._parent;
			while ( parent != null ) {
				if ( parent.IsA(type) ) {
					return parent;
				}
			}
			return null;
		}

		public void ClearAllChildren() {
			while ( _children.Count > 0 ) {
				_children[0].Destroy();
			}
		}

		public virtual void Destroy() {
			this.Parent = null;
			ClearAllChildren();
		}

	}
}
