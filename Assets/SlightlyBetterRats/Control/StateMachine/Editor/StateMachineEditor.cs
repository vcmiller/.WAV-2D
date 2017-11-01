using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class StateMachineEditor : EditorWindow {
    private Operation op;
    private bool dirty = false;
    private float viewportWidth;
    private bool resize = false;

    [NonSerialized]
    public static StateMachineDefinition def;

    [NonSerialized]
    private StateMachineDefinition.Transition lastSelectedTr;
    [NonSerialized]
    private StateMachineDefinition.State lastSelectedState;

    [NonSerialized]
    private StateMachineDefinition.State editingState;
    [NonSerialized]
    private Pair<StateMachineDefinition.State, StateMachineDefinition.Transition> editingTransition;

    Vector2 scrollPos = Vector2.zero;

    public StateMachineEditor() {
        wantsMouseMove = true;
        editingState = null;
        editingTransition = new Pair<StateMachineDefinition.State, StateMachineDefinition.Transition>(null, null);
    }

    [MenuItem("Window/State Machine Editor")]
    public static void ShowWindow() {
        GetWindow(typeof(StateMachineEditor));
    }

    void UpdateSelection() {
        Repaint();
    }

    void OnGUI() {
        ResizeScrollView();
        Rect viewportRect = new Rect(0, 0, viewportWidth, position.height);
        EditorGUI.DrawRect(viewportRect, new Color32(93, 93, 93, 255));
        bool repaint = false;

        foreach (var obj in Selection.objects) {
            if (obj is StateMachineDefinition) {
                def = (StateMachineDefinition)obj;
                break;
            }
        }

        if (def != null) {
            bool showSide = true;

            if (dirty && GUI.Button(new Rect(10, 10, 100, 30), "Save")) {
                AssetDatabase.SaveAssets();
                dirty = false;
            }

            Event cur = Event.current;

            if (op != null) {
                if (def != op.definition) {
                    op.Cancel();
                    op = null;
                } else {
                    op.Update();

                    if (op.done) {
                        op = null;
                        repaint = true;
                        EditorUtility.SetDirty(def);
                        dirty = true;
                    }
                }
            } else {
                var selected = def.SelectState(cur.mousePosition);
                var selectedTr = def.SelectTransition(cur.mousePosition);

                if (selected == null) {
                    if (selectedTr.t2 != lastSelectedTr) {
                        repaint = true;
                        lastSelectedTr = selectedTr.t2;
                    }

                    if (lastSelectedState != null) {
                        lastSelectedState = null;
                        repaint = true;
                    }
                } else {
                    if (selected != lastSelectedState) {
                        repaint = true;
                        lastSelectedState = selected;
                    }

                    if (lastSelectedTr != null) {
                        lastSelectedTr = null;
                        repaint = true;
                    }
                }

                if (viewportRect.Contains(cur.mousePosition)) {
                    if (cur.type == EventType.MouseDown) {
                        if (cur.button == 0) {
                            if (selected != null) {
                                editingState = selected;
                                editingTransition.t1 = null;
                                editingTransition.t2 = null;
                                repaint = true;
                                showSide = false;


                                if (cur.clickCount == 1) {
                                    op = new MoveStateOperation(def, selected);
                                } else if (cur.clickCount == 2) {
                                    op = new RenameStateOperation(def, selected);
                                }
                            } else if (selectedTr.t1 != null) {
                                editingTransition = selectedTr;
                                editingState = null;
                                repaint = true;
                                showSide = false;
                            } else {
                                editingState = null;
                                editingTransition.t1 = null;
                                editingTransition.t2 = null;
                                repaint = true;
                                showSide = false;
                            }
                        } else if (cur.button == 1) {
                            if (selected == null && lastSelectedTr == null) {
                                GenericMenu menu = new GenericMenu();

                                menu.AddItem(new GUIContent("Create State"), false, () => {
                                    var s = def.AddState();
                                    s.position = cur.mousePosition;
                                    op = new RenameStateOperation(def, s);
                                    EditorUtility.SetDirty(def);
                                    dirty = true;
                                });

                                menu.ShowAsContext();
                            } else if (selected != null) {
                                GenericMenu menu = new GenericMenu();

                                menu.AddItem(new GUIContent("Delete State"), false, () => {
                                    def.RemoveState(selected);
                                    EditorUtility.SetDirty(def);
                                    dirty = true;
                                    repaint = true;
                                });

                                menu.AddItem(new GUIContent("Add Transition"), false, () => {
                                    op = new MakeTransitionOperation(def, selected);
                                });

                                menu.ShowAsContext();
                            } else if (selectedTr.t1 != null) {
                                GenericMenu menu = new GenericMenu();

                                menu.AddItem(new GUIContent("Remove Transition"), false, () => {
                                    selectedTr.t1.RemoveTransition(selectedTr.t2);
                                    EditorUtility.SetDirty(def);
                                    dirty = true;
                                    repaint = true;
                                });

                                menu.ShowAsContext();
                            }
                        }
                    }
                }
            }

            if (Event.current.type != EventType.Repaint && (repaint || (op != null && op.repaint))) {
                Repaint();
            }
            
            Handles.BeginGUI();

            foreach (var from in def.states) {
                if (from.transitions != null) {
                    foreach (var tr in from.transitions) {
                        if (tr != lastSelectedTr && tr != editingTransition.t2) {
                            Handles.color = Color.black;
                        } else {
                            Handles.color = Color.blue;
                        }

                        if (def.GetState(tr.to) != null) {
                            var line = def.GetTransitionPoints(from, tr);
                            Vector2 src = line.t1;
                            Vector2 dest = line.t2;

                            Vector2 v = (dest - src).normalized;
                            Vector2 ortho = new Vector2(v.y, -v.x);

                            Vector2 arrow = ortho - v;
                            Vector2 mid = (src + dest) / 2;

                            Handles.DrawAAPolyLine(3, src, dest);
                            Handles.DrawAAPolyLine(3, mid + v * 5, mid + arrow * 10);
                        }
                    }
                }
            }

            Handles.EndGUI();

            foreach (var state in def.states) {
                if (op == null || op.state != state || op.showBaseGUI) {
                    if (state != lastSelectedState && state != editingState) {
                        GUI.Button(state.rect, state.name);
                    } else {
                        GUI.Button(state.rect, "");

                        var centeredStyle = GUI.skin.GetStyle("Label");
                        centeredStyle.alignment = TextAnchor.MiddleCenter;
                        centeredStyle.normal.textColor = Color.blue;
                        centeredStyle.fontStyle = FontStyle.Bold;

                        GUI.Label(state.rect, state.name, centeredStyle);
                    }
                }

                if (op != null && op.state == state) {
                    op.OnGUI();
                }
            }

            if (showSide) {
                EditorGUI.DrawRect(new Rect(viewportWidth, 0, position.width - viewportWidth, position.height), new Color32(194, 194, 194, 255));


                float padding = 20;
                GUILayout.BeginArea(new Rect(viewportWidth + padding, padding, position.width - viewportWidth - padding * 2, position.height - padding * 2));
                EditorGUILayout.BeginVertical();
                EditorGUIUtility.labelWidth = 1;


                if (editingState != null) {
                    EditorGUILayout.LabelField("State " + editingState.name);
                    EditorGUILayout.LabelField("State Events", EditorStyles.boldLabel);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Enter: ");
                    bool enter = EditorGUILayout.Toggle(editingState.hasEnter);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("During: ");
                    bool during = EditorGUILayout.Toggle(editingState.hasDuring);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Exit: ");
                    bool exit = EditorGUILayout.Toggle(editingState.hasExit);
                    EditorGUILayout.EndHorizontal();


                    if (enter != editingState.hasEnter || during != editingState.hasDuring || exit != editingState.hasExit) {
                        editingState.hasEnter = enter;
                        editingState.hasDuring = during;
                        editingState.hasExit = exit;

                        dirty = true;
                        EditorUtility.SetDirty(def);
                    }

                } else if (editingTransition.t1 != null) {
                    EditorGUILayout.LabelField("Transition From " + editingTransition.t1.name + " To " + editingTransition.t2.to);
                    EditorGUILayout.LabelField("Transition Events", EditorStyles.boldLabel);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Notify: ");
                    bool notify = EditorGUILayout.Toggle(editingTransition.t2.hasNotify);
                    EditorGUILayout.EndHorizontal();

                    if (notify != editingTransition.t2.hasNotify) {
                        editingTransition.t2.hasNotify = notify;

                        dirty = true;
                        EditorUtility.SetDirty(def);
                    }
                }

                EditorGUILayout.EndVertical();

                GUILayout.EndArea();
            }
        } else if (op != null) {
            op.Cancel();
            op = null;
        }
        
    }

    private void OnEnable() {
        viewportWidth = position.width * 0.8f;

        Selection.selectionChanged += UpdateSelection;
    }

    private void OnDisable() {
        Selection.selectionChanged -= UpdateSelection;
    }

    private void ResizeScrollView() {
        Rect cursorChangeRect = new Rect(viewportWidth, 0, 5f, position.height);

        EditorGUIUtility.AddCursorRect(cursorChangeRect, MouseCursor.ResizeHorizontal);

        if (Event.current.type == EventType.mouseDown && cursorChangeRect.Contains(Event.current.mousePosition)) {
            resize = true;
        }
        if (resize) {
            viewportWidth = Event.current.mousePosition.x;
            cursorChangeRect.Set(viewportWidth, cursorChangeRect.y, cursorChangeRect.width, cursorChangeRect.height);
            Repaint();
        }
        if (Event.current.type == EventType.MouseUp)
            resize = false;
    }
}
