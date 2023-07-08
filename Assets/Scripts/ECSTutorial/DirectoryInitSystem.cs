using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UI;

namespace ECSTutorial
{
    public partial struct DirectoryInitSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<LocalTransform>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            // Note: 2フレーム目からUpdateが走らないようにしている
            //       OnCreateほぼ同様の挙動になるが、OnCreateは実行してからSceneロード以前に呼ばれるので、Sceneがロードされてから処理したい場合には、
            //       OnCreateで何かしらをRequireForUpdateで指定して、Sceneロード後にOnUpdateが呼ばれるようにし、state.Enabled = false すれば良い
            state.Enabled = false;
            
            var go = GameObject.Find("Directory");
            if (go == null)
            {
                throw new System.Exception("GameObject 'Directory' not found.");
            }

            var directory = go.GetComponent<Directory>();

            var directoryManager = new DirectoryManager();
            directoryManager.rotatorPrefab = directory.rotatorPrefab;
            directoryManager.rotationToggle = directory.rotationToggle;

            var entity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity, directoryManager);
        }
    }

    public class DirectoryManager : IComponentData
    {
        public GameObject rotatorPrefab;
        public Toggle rotationToggle;
    }
}