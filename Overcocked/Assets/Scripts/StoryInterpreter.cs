﻿// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryInterpreter : MonoBehaviour
{
  public Canvas canvas;
  public TextAsset initialStory;

  private List<Command> commands;

  void Start()
  {
    commands = LoadStoryCommands(initialStory);
    ExecuteCommand();
  }

  private List<Command> LoadStoryCommands(string storyName)
  {
    TextAsset story = Resources.Load($"Stories/{storyName}") as TextAsset;
    return LoadStoryCommands(story);
  }

  private List<Command> LoadStoryCommands(TextAsset story)
  {
    StoryData storyData = JsonUtility.FromJson<StoryData>(story.text);
    return storyData.commandData.Select(CommandFactory.CreateCommand).ToList();
  }

  private void ExecuteCommand()
  {
    if (commands.Count > 0)
    {
      commands[0].OnCommandExecuted += ExecuteCommand;
      commands[0].Execute(canvas);
      commands.RemoveAt(0);
    }
  }
}