using ACMsbEditor.Binding.Msb;
using SoulsFormats;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AcMsbEditor.Binding.Nodes
{
    public class Ac4MsbNodeBinding : IMsbNodeBinding
    {
        private static readonly Dictionary<string, string[]> Types = new()
        {
                { "Models", ["MapPiece", "Object", "Enemy", "Dummy"] },
                { "Events", ["Script", "Effect", "Scene", "DummyLeader", "BGM", "Rev", "SFX"] },
                { "Points", ["Distance", "RoutePoint", "Action", "OperationalArea", "WarningArea", "AttentionArea", "DeathField", "Spawn", "Camera", "SFX", "NoTurretArea"] },
                { "Routes", ["Default", "AI"] },
                { "Layers", ["Layer"] },
                { "Parts", ["MapPiece", "Object", "Enemy", "Unused"] },
                // { "MapStudioTree", ["Node"] }
        };

        private static readonly string[] MsbParamTypes = [.. Types.Keys];

        public MsbGame Game => MsbGame.AC4;
        private readonly MSBAC4 Msb;

        public Ac4MsbNodeBinding(MSBAC4 msb)
        {
            Msb = msb;
        }

        #region Entry

        private static bool AddEntry<T>(List<T> list, T entry) where T : IMsbEntry
        {
            if (list.Find(e => e.Name.Equals(entry.Name)) == null)
            {
                list.Add(entry);
                return true;
            }

            return false;
        }

        private static bool RemoveEntry<T>(List<T> list, string entryName) where T : IMsbEntry
        {
            if (list.RemoveAll(e => e.Name.Equals(entryName)) > 0)
            {
                return true;
            }

            return false;
        }

        private static T? GetDuplicateEntryName<T>(List<T> list, string entryName, out string dupEntryName) where T : IMsbEntry
        {
            var entry = list.Find(e => e.Name.Equals(entryName));
            if (entry == null)
            {
                dupEntryName = string.Empty;
                return entry;
            }

            int index = 0;
            string newName = $"{entryName}_{index}";
            while (list.Find(e => e.Name.Equals(newName)) != null)
            {
                if (index == int.MaxValue)
                {
                    dupEntryName = string.Empty;
                    return entry;
                }

                index++;
                newName = $"{entryName}_{index}";
            }

            dupEntryName = newName;
            return entry;
        }

        #endregion

        #region Add

        public bool AddMsbEntry(string paramType, string entryType, string entryName)
        {
            switch (paramType)
            {
                case "Models":
                    return AddModel(entryName, entryType);
                case "Events":
                    return AddEvent(entryName, entryType);
                case "Points":
                    return AddPoint(entryName, entryType);
                case "Routes":
                    return AddRoute(entryName, entryType);
                case "Layers":
                    return AddLayer(entryName, entryType);
                case "Parts":
                    return AddPart(entryName, entryType);
                default:
                    return false;
            }
        }

        private bool AddModel(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "MapPiece":
                    return AddEntry(Msb.Models.MapPieces, new MSBAC4.Model.MapPiece(entryName));
                case "Object":
                    return AddEntry(Msb.Models.Objects, new MSBAC4.Model.Object(entryName));
                case "Enemy":
                    return AddEntry(Msb.Models.Enemies, new MSBAC4.Model.Enemy(entryName));
                case "Dummy":
                    return AddEntry(Msb.Models.Dummies, new MSBAC4.Model.Dummy(entryName));
                default:
                    return false;
            }
        }

        private bool AddEvent(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "Script":
                    return AddEntry(Msb.Events.Scripts, new MSBAC4.Event.Script(entryName));
                case "Effect":
                    return AddEntry(Msb.Events.Effects, new MSBAC4.Event.Effect(entryName));
                case "Scene":
                    return AddEntry(Msb.Events.Scenes, new MSBAC4.Event.Scene(entryName));
                case "DummyLeader":
                    return AddEntry(Msb.Events.DummyLeaders, new MSBAC4.Event.DummyLeader(entryName));
                case "BGM":
                    return AddEntry(Msb.Events.BGMs, new MSBAC4.Event.BGM(entryName));
                case "Rev":
                    return AddEntry(Msb.Events.Revs, new MSBAC4.Event.Rev(entryName));
                case "SFX":
                    return AddEntry(Msb.Events.SFXs, new MSBAC4.Event.SFX(entryName));
                default:
                    return false;
            }
        }

        private bool AddPoint(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "Distance":
                    return AddEntry(Msb.Regions.DistanceRegions, new MSBAC4.Region.DistanceRegion(entryName));
                case "RoutePoint":
                    return AddEntry(Msb.Regions.RoutePoints, new MSBAC4.Region.RoutePoint(entryName));
                case "Action":
                    return AddEntry(Msb.Regions.Actions, new MSBAC4.Region.Action(entryName));
                case "OperationalArea":
                    return AddEntry(Msb.Regions.OperationalAreas, new MSBAC4.Region.OperationalArea(entryName));
                case "WarningArea":
                    return AddEntry(Msb.Regions.WarningAreas, new MSBAC4.Region.WarningArea(entryName));
                case "AttentionArea":
                    return AddEntry(Msb.Regions.AttentionAreas, new MSBAC4.Region.AttentionArea(entryName));
                case "DeathField":
                    return AddEntry(Msb.Regions.DeathFields, new MSBAC4.Region.DeathField(entryName));
                case "Spawn":
                    return AddEntry(Msb.Regions.SpawnPoints, new MSBAC4.Region.SpawnPoint(entryName));
                case "Camera":
                    return AddEntry(Msb.Regions.CameraPoints, new MSBAC4.Region.CameraPoint(entryName));
                case "SFX":
                    return AddEntry(Msb.Regions.SFXRegions, new MSBAC4.Region.SFXRegion(entryName));
                case "NoTurretArea":
                    return AddEntry(Msb.Regions.NoTurretAreas, new MSBAC4.Region.NoTurretAreaRegion(entryName));
                default:
                    return false;
            }
        }

        private bool AddRoute(string entryName, string entryType)
        {
            if (entryName == null || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "Default":
                    Msb.Routes.DefaultRoutes.Add(new MSBAC4.Route.DefaultRoute(entryName));
                    return true;
                case "AI":
                    Msb.Routes.AIRoutes.Add(new MSBAC4.Route.AIRoute(entryName));
                    return true;
                default:
                    return false;
            }
        }

        private bool AddLayer(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "Layer":
                    return AddEntry(Msb.Layers.Layers, new MSBAC4.Layer(entryName));
                default:
                    return false;
            }
        }

        private bool AddPart(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "MapPiece":
                    return AddEntry(Msb.Parts.MapPieces, new MSBAC4.Part.MapPiece(entryName));
                case "Object":
                    return AddEntry(Msb.Parts.Objects, new MSBAC4.Part.Object(entryName));
                case "Enemy":
                    return AddEntry(Msb.Parts.Enemies, new MSBAC4.Part.Enemy(entryName));
                case "Unused":
                    return AddEntry(Msb.Parts.Unused, new MSBAC4.Part.Unused(entryName));
                default:
                    return false;
            }
        }

        #endregion

        #region Remove

        public bool RemoveMsbEntry(string paramType, string entryType, string entryName)
        {
            switch (paramType)
            {
                case "Models":
                    return RemoveModel(entryName, entryType);
                case "Events":
                    return RemoveEvent(entryName, entryType);
                case "Points":
                    return RemovePoint(entryName, entryType);
                case "Routes":
                    return RemoveRoute(entryName, entryType);
                case "Layers":
                    return RemoveLayer(entryName, entryType);
                case "Parts":
                    return RemovePart(entryName, entryType);
                default:
                    return false;
            }
        }

        private bool RemoveModel(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "MapPiece":
                    return RemoveEntry(Msb.Models.MapPieces, entryName);
                case "Object":
                    return RemoveEntry(Msb.Models.Objects, entryName);
                case "Enemy":
                    return RemoveEntry(Msb.Models.Enemies, entryName);
                case "Dummy":
                    return RemoveEntry(Msb.Models.Dummies, entryName);
                default:
                    return false;
            }
        }

        private bool RemoveEvent(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "Script":
                    return RemoveEntry(Msb.Events.Scripts, entryName);
                case "Effect":
                    return RemoveEntry(Msb.Events.Effects, entryName);
                case "Scene":
                    return RemoveEntry(Msb.Events.Scenes, entryName);
                case "DummyLeader":
                    return RemoveEntry(Msb.Events.DummyLeaders, entryName);
                case "BGM":
                    return RemoveEntry(Msb.Events.BGMs, entryName);
                case "Rev":
                    return RemoveEntry(Msb.Events.Revs, entryName);
                case "SFX":
                    return RemoveEntry(Msb.Events.SFXs, entryName);
                default:
                    return false;
            }
        }

        private bool RemovePoint(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "Distance":
                    return RemoveEntry(Msb.Regions.DistanceRegions, entryName);
                case "RoutePoint":
                    return RemoveEntry(Msb.Regions.RoutePoints, entryName);
                case "Action":
                    return RemoveEntry(Msb.Regions.Actions, entryName);
                case "OperationalArea":
                    return RemoveEntry(Msb.Regions.OperationalAreas, entryName);
                case "WarningArea":
                    return RemoveEntry(Msb.Regions.WarningAreas, entryName);
                case "AttentionArea":
                    return RemoveEntry(Msb.Regions.AttentionAreas, entryName);
                case "DeathField":
                    return RemoveEntry(Msb.Regions.DeathFields, entryName);
                case "Spawn":
                    return RemoveEntry(Msb.Regions.SpawnPoints, entryName);
                case "Camera":
                    return RemoveEntry(Msb.Regions.CameraPoints, entryName);
                case "SFX":
                    return RemoveEntry(Msb.Regions.SFXRegions, entryName);
                case "NoTurretArea":
                    return RemoveEntry(Msb.Regions.NoTurretAreas, entryName);
                default:
                    return false;
            }
        }

        private bool RemoveRoute(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "Default":
                    RemoveEntry(Msb.Routes.DefaultRoutes, entryName);
                    return true;
                case "AI":
                    RemoveEntry(Msb.Routes.AIRoutes, entryName);
                    return true;
                default:
                    return false;
            }
        }

        private bool RemoveLayer(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "Layer":
                    RemoveEntry(Msb.Layers.Layers, entryName);
                    return true;
                default:
                    return false;
            }
        }

        private bool RemovePart(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            switch (entryType)
            {
                case "MapPiece":
                    return RemoveEntry(Msb.Parts.MapPieces, entryName);
                case "Object":
                    return RemoveEntry(Msb.Parts.Objects, entryName);
                case "Enemy":
                    return RemoveEntry(Msb.Parts.Enemies, entryName);
                case "Unused":
                    return RemoveEntry(Msb.Parts.Unused, entryName);
                default:
                    return false;
            }
        }

        #endregion

        #region Duplicate

        public bool DuplicateMsbEntry(string paramType, string entryType, string entryName)
        {
            switch (paramType)
            {
                case "Models":
                    return DuplicateModel(entryName, entryType);
                case "Events":
                    return DuplicateEvent(entryName, entryType);
                case "Points":
                    return DuplicatePoint(entryName, entryType);
                case "Routes":
                    return DuplicateRoute(entryName, entryType);
                case "Layers":
                    return DuplicateLayer(entryName, entryType);
                case "Parts":
                    return DuplicatePart(entryName, entryType);
                default:
                    return false;
            }
        }

        private bool DuplicateModel(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            MSBAC4.Model? copy;
            string? dupEntryName;
            switch (entryType)
            {
                case "MapPiece":
                    copy = (GetDuplicateEntryName(Msb.Models.MapPieces, entryName, out dupEntryName) as MSBAC4.Model)?.DeepCopy();
                    break;
                case "Object":
                    copy = (GetDuplicateEntryName(Msb.Models.Objects, entryName, out dupEntryName) as MSBAC4.Model)?.DeepCopy();
                    break;
                case "Enemy":
                    copy = (GetDuplicateEntryName(Msb.Models.Enemies, entryName, out dupEntryName) as MSBAC4.Model)?.DeepCopy();
                    break;
                case "Dummy":
                    copy = (GetDuplicateEntryName(Msb.Models.Dummies, entryName, out dupEntryName) as MSBAC4.Model)?.DeepCopy();
                    break;
                default:
                    return false;
            }

            if (copy != null)
            {
                copy.Name = dupEntryName;
                Msb.Models.Add(copy);
                return true;
            }

            return false;
        }

        private bool DuplicateEvent(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            MSBAC4.Event? copy;
            string? dupEntryName;
            switch (entryType)
            {
                case "Script":
                    copy = (GetDuplicateEntryName(Msb.Events.Scripts, entryName, out dupEntryName) as MSBAC4.Event)?.DeepCopy();
                    break;
                case "Effect":
                    copy = (GetDuplicateEntryName(Msb.Events.Effects, entryName, out dupEntryName) as MSBAC4.Event)?.DeepCopy();
                    break;
                case "Scene":
                    copy = (GetDuplicateEntryName(Msb.Events.Scenes, entryName, out dupEntryName) as MSBAC4.Event)?.DeepCopy();
                    break;
                case "DummyLeader":
                    copy = (GetDuplicateEntryName(Msb.Events.DummyLeaders, entryName, out dupEntryName) as MSBAC4.Event)?.DeepCopy();
                    break;
                case "BGM":
                    copy = (GetDuplicateEntryName(Msb.Events.BGMs, entryName, out dupEntryName) as MSBAC4.Event)?.DeepCopy();
                    break;
                case "Rev":
                    copy = (GetDuplicateEntryName(Msb.Events.Revs, entryName, out dupEntryName) as MSBAC4.Event)?.DeepCopy();
                    break;
                case "SFX":
                    copy = (GetDuplicateEntryName(Msb.Events.SFXs, entryName, out dupEntryName) as MSBAC4.Event)?.DeepCopy();
                    break;
                default:
                    return false;
            }

            if (copy != null)
            {
                copy.Name = dupEntryName;
                Msb.Events.Add(copy);
                return true;
            }

            return false;
        }

        private bool DuplicatePoint(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            MSBAC4.Region? copy;
            string? dupEntryName;
            switch (entryType)
            {
                case "Distance":
                    copy = (GetDuplicateEntryName(Msb.Regions.DistanceRegions, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "RoutePoint":
                    copy = (GetDuplicateEntryName(Msb.Regions.RoutePoints, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "Action":
                    copy = (GetDuplicateEntryName(Msb.Regions.Actions, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "OperationalArea":
                    copy = (GetDuplicateEntryName(Msb.Regions.OperationalAreas, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "WarningArea":
                    copy = (GetDuplicateEntryName(Msb.Regions.WarningAreas, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "AttentionArea":
                    copy = (GetDuplicateEntryName(Msb.Regions.AttentionAreas, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "DeathField":
                    copy = (GetDuplicateEntryName(Msb.Regions.DeathFields, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "Spawn":
                    copy = (GetDuplicateEntryName(Msb.Regions.SpawnPoints, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "Camera":
                    copy = (GetDuplicateEntryName(Msb.Regions.CameraPoints, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "SFX":
                    copy = (GetDuplicateEntryName(Msb.Regions.SFXRegions, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                case "NoTurretArea":
                    copy = (GetDuplicateEntryName(Msb.Regions.NoTurretAreas, entryName, out dupEntryName) as MSBAC4.Region)?.DeepCopy();
                    break;
                default:
                    return false;
            }

            if (copy != null)
            {
                copy.Name = dupEntryName;
                Msb.Regions.Add(copy);
                return true;
            }

            return false;
        }

        private bool DuplicateRoute(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            MSBAC4.Route? copy;
            string? dupEntryName;
            switch (entryType)
            {
                case "Default":
                    copy = (GetDuplicateEntryName(Msb.Routes.DefaultRoutes, entryName, out dupEntryName) as MSBAC4.Route)?.DeepCopy();
                    break;
                case "AI":
                    copy = (GetDuplicateEntryName(Msb.Routes.AIRoutes, entryName, out dupEntryName) as MSBAC4.Route)?.DeepCopy();
                    break;
                default:
                    return false;
            }

            if (copy != null)
            {
                copy.Name = dupEntryName;
                Msb.Routes.Add(copy);
                return true;
            }

            return false;
        }

        private bool DuplicateLayer(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            MSBAC4.Layer? copy;
            string? dupEntryName;
            switch (entryType)
            {
                case "Layer":
                    copy = GetDuplicateEntryName(Msb.Layers.Layers, entryName, out dupEntryName)?.DeepCopy();
                    break;
                default:
                    return false;
            }

            if (copy != null)
            {
                copy.Name = dupEntryName;
                Msb.Layers.Layers.Add(copy);
                return true;
            }

            return false;
        }

        private bool DuplicatePart(string entryName, string entryType)
        {
            if (string.IsNullOrWhiteSpace(entryName) || string.IsNullOrWhiteSpace(entryType))
            {
                return false;
            }

            MSBAC4.Part? copy;
            string? dupEntryName;
            switch (entryType)
            {
                case "MapPiece":
                    copy = (GetDuplicateEntryName(Msb.Parts.MapPieces, entryName, out dupEntryName) as MSBAC4.Part)?.DeepCopy();
                    break;
                case "Object":
                    copy = (GetDuplicateEntryName(Msb.Parts.Objects, entryName, out dupEntryName) as MSBAC4.Part)?.DeepCopy();
                    break;
                case "Enemy":
                    copy = (GetDuplicateEntryName(Msb.Parts.Enemies, entryName, out dupEntryName) as MSBAC4.Part)?.DeepCopy();
                    break;
                case "Unused":
                    copy = (GetDuplicateEntryName(Msb.Parts.Unused, entryName, out dupEntryName) as MSBAC4.Part)?.DeepCopy();
                    break;
                default:
                    return false;
            }

            if (copy != null)
            {
                copy.Name = dupEntryName;
                Msb.Parts.Add(copy);
                return true;
            }

            return false;
        }

        #endregion

        #region Get Data

        public IEnumerable<MsbParamNode> GetMsbParams()
        {
            var param = MsbParamNode.Create("Models");
            param.AddEntries(Msb.Models.MapPieces, "MapPiece");
            param.AddEntries(Msb.Models.Objects, "Object");
            param.AddEntries(Msb.Models.Enemies, "Enemy");
            param.AddEntries(Msb.Models.Dummies, "Dummy");
            yield return param;

            param = MsbParamNode.Create("Events");
            param.AddEntries(Msb.Events.Scripts, "Script");
            param.AddEntries(Msb.Events.Effects, "Effect");
            param.AddEntries(Msb.Events.Scenes, "Scene");
            param.AddEntries(Msb.Events.DummyLeaders, "DummyLeader");
            param.AddEntries(Msb.Events.BGMs, "BGM");
            param.AddEntries(Msb.Events.Revs, "Rev");
            param.AddEntries(Msb.Events.SFXs, "SFX");
            yield return param;

            param = MsbParamNode.Create("Points");
            param.AddEntries(Msb.Regions.DistanceRegions, "Distance");
            param.AddEntries(Msb.Regions.RoutePoints, "RoutePoint");
            param.AddEntries(Msb.Regions.Actions, "Action");
            param.AddEntries(Msb.Regions.OperationalAreas, "OperationalArea");
            param.AddEntries(Msb.Regions.WarningAreas, "WarningArea");
            param.AddEntries(Msb.Regions.AttentionAreas, "AttentionArea");
            param.AddEntries(Msb.Regions.DeathFields, "DeathField");
            param.AddEntries(Msb.Regions.SpawnPoints, "Spawn");
            param.AddEntries(Msb.Regions.CameraPoints, "Camera");
            param.AddEntries(Msb.Regions.SFXRegions, "SFX");
            param.AddEntries(Msb.Regions.NoTurretAreas, "NoTurretArea");
            yield return param;

            param = MsbParamNode.Create("Routes");
            param.AddEntries(Msb.Routes.DefaultRoutes, "Default");
            param.AddEntries(Msb.Routes.AIRoutes, "AI");
            yield return param;

            param = MsbParamNode.Create("Layers");
            param.AddEntries(Msb.Layers.Layers, "Layer");
            yield return param;

            param = MsbParamNode.Create("Parts");
            param.AddEntries(Msb.Parts.MapPieces, "MapPiece");
            param.AddEntries(Msb.Parts.Objects, "Object");
            param.AddEntries(Msb.Parts.Enemies, "Enemy");
            param.AddEntries(Msb.Parts.Unused, "Unused");
            yield return param;
        }

        #endregion

        #region Get Types

        public bool GetMsbEntryTypes(string paramType, [NotNullWhen(true)] out string[]? types)
            => Types.TryGetValue(paramType, out types);

        public string[] GetMsbParamTypes()
            => MsbParamTypes;

        #endregion

    }
}
