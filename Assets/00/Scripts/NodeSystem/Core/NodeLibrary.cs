using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class NodeLibrary : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private bool _debugLog = false;
    [SerializeField] private NodeCanvas _nodeCanvasPrefab;
    #endregion


    #region Public Properties
    public static NodeLibrary Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NodeLibrary>(true);
                if (_instance == null)
                {
                    _instance = new GameObject("NodeLibrary").AddComponent<NodeLibrary>();
                }
            }

            return _instance;
        }
    }

    public Dictionary<string, Type> NodeTypes => _nodeTypes;
    #endregion


    #region Events
    public event Action OnNodeLibraryInitialized;
    #endregion


    #region Internal Variables
    private static NodeLibrary _instance;
    private Dictionary<string, Type> _nodeTypes = new();
    private List<NodeCanvas> _sceneCanvases = new();
    #endregion


    #region Data Constructs
    #endregion


    #region MonoBehaviour Loop
    private void Awake()
    {
        _instance = this;
        InitializeNodeTypes();
    }

    private void Start()
    {
        _sceneCanvases = FindObjectsOfType<NodeCanvas>(true).ToList();
    }
    #endregion


    #region Internal Functions
    private static IEnumerable<Type> GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal));
    }
    private void InitializeNodeTypes()
    {
        Type[] nodeLibraryClasses = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Nodes.Library").ToArray();

        string allNodesDebugString = "Initialized all node types: \n";
        
        foreach (Type nodeClassType in nodeLibraryClasses)
        {
            NodeAttribute menuItemAttribute = nodeClassType.GetCustomAttribute<NodeAttribute>();
                
            if (menuItemAttribute != null)
            {
                string[] path = menuItemAttribute.MenuTitle.Split('/');

                allNodesDebugString += path.Last() + "\n";
                _nodeTypes.Add(menuItemAttribute.MenuTitle, nodeClassType);
            }
        }

        Debug.Log(allNodesDebugString);
        OnNodeLibraryInitialized?.Invoke();
    }
    #endregion


    #region Public API
    public void SaveToFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string folderPath = Path.Combine(Application.persistentDataPath, "AllCanvasData");
        foreach (NodeCanvas nodeCanvas in _sceneCanvases)
        {
            FileStream fs = File.Create(folderPath + "/NodeCanvasData" + nodeCanvas._guid + ".ncd");
            NodeCanvasSaveData saveData = nodeCanvas.GetSaveData();
            bf.Serialize(fs, saveData);
            fs.Close();
        }
    }
    public void LoadFromFile()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "AllCanvasData");
        string[] filePaths = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
        BinaryFormatter bf = new BinaryFormatter();
        if (filePaths.Length != 0)
        {
            List<NodeCanvasSaveData> saveDataList = new();
            foreach (string filePath in filePaths)
            {
                byte[] data = File.ReadAllBytes(filePath);
                using (var stream = new MemoryStream(data))
                {
                    saveDataList.Add((NodeCanvasSaveData)bf.Deserialize(stream));
                }
            }

            foreach (NodeCanvasSaveData saveData in saveDataList)
            {
                //TODO: finish loading here
            }
        }
    }

    public NodeCanvas GetFreshCanvas(string guid = "")
    {
        NodeCanvas nodeCanvas = Instantiate(_nodeCanvasPrefab);
        if (string.IsNullOrEmpty(guid) || string.IsNullOrWhiteSpace(guid)) return nodeCanvas;
        nodeCanvas.SetGuid(guid);
        return nodeCanvas;
    }
    #endregion
}
