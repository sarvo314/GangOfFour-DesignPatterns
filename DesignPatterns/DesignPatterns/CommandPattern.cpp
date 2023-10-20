#include<stack>
#include<iostream>
#include<memory>
using namespace std; 
//reference
// https://gernotklingler.com/blog/implementing-undoredo-with-the-command-pattern/
class ICommand
{
  public:
    virtual void execute() = 0;
    virtual void undo() = 0;
    virtual void redo() = 0;
};


class Tv
{
  bool mOn;
  int mChannel;

  public:
    Tv(){}

    void switchOn() {mOn = true;}
    void switchOff() {mOn = false;}
    
    void switchChannel(int channel)
    {
      mChannel = channel;
    }

    bool isOn() {return mOn;}
    int getChannel() {return mChannel;}
};

class TvOnCommand : public ICommand
{
  Tv *mTv;
  public:
    TvOnCommand(Tv &tv):mTv(&tv) {}
    void execute() {mTv->switchOn();}
    void undo() {mTv->switchOff();}
    void redo() {mTv->switchOn();}

};

class TvOffCommand : public ICommand
{
  TvOnCommand mTvOnCommand; //reuse the old code from TvOnCommand
  public:
    TvOffCommand(Tv &tv):mTvOnCommand(tv) {}
    void execute() {mTvOnCommand.undo();}
    void undo() {mTvOnCommand.execute();}
    void redo() {mTvOnCommand.undo();}
};

class TvSwitchChannelCommand : public ICommand
{
  Tv *mTv;
  int mOldChannel;
  int mNewChannel;
  public:
    TvSwitchChannelCommand(Tv *tv, int channel)
    {
      mTv = tv;
      mNewChannel = channel;
    }

    void execute()
    {
      cout<<endl<<"Switched from "<<mOldChannel<<" to "<<mNewChannel<<endl;
      mOldChannel = mTv->getChannel();
      mTv->switchChannel(mNewChannel);
    }

    void undo()
    {
      mTv->switchChannel(mOldChannel);
    }
    void redo()
    {
      mTv->switchChannel(mNewChannel);
    }
};

typedef std::stack<std::shared_ptr<ICommand> > commandStack_t;

class CommandManager
{
  commandStack_t mUndoStack;
  commandStack_t mRedoStack;

  public:
  CommandManager() {}
  void executeCmd(std::shared_ptr<ICommand> command)
  {
    mRedoStack = commandStack_t(); //clear the redo stack
    command->execute();
    mUndoStack.push(command);
  }

  void undo()
  {
    if(mUndoStack.size() <= 0)
    {
      return;
    }
    mUndoStack.top()->undo();
    mRedoStack.push(mUndoStack.top());
    mUndoStack.pop();
  }

  void redo()
  {
    if(mRedoStack.size() <= 0)
    {
      return;
    }
    mRedoStack.top()->redo();
    mUndoStack.push(mRedoStack.top());
    mRedoStack.pop();
  } 
};

int main()
{
  using namespace std;

  Tv tv;

  CommandManager CommandManager;

  shared_ptr<ICommand> c1(new TvSwitchChannelCommand(&tv, 1));
  CommandManager.executeCmd(c1);
}

