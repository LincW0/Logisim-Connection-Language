#include <QtWidgets>
#include <QtCore/QCoreApplication>
#include <QSettings>
#include <string.h>

#include "mainwindow.h"

MainWindow::MainWindow(QWidget *parent):QMainWindow(parent)
{
    setupGUI();
}

void MainWindow::compileFile()
{
    QProcess process;
    int result=process.execute(".\\compiler.exe \""+fileName+"\"");
    QString Result;
    Result=QString::fromStdString(std::string((result==0)?"Successfully Compiled.":("Compilation Error\nCode:"+std::to_string(result))));
    QMessageBox::information(this,tr("Compilation result"),Result);
    //QSettings settings=new QSettings("setting.ini",QSettings::IniFormat);
}
void MainWindow::runFile()
{
    system(("start E:\\GitHub\\Logisim-Connection-Language\\IDE\\build-IDE-Desktop_Qt_5_12_9_MinGW_64_bit-Release\\release\\logisim-win-2.7.1.exe \""+(((fileName.toStdString()).substr(0,(fileName.toStdString()).find_last_of('.')))+".circ")).c_str());
}
void MainWindow::compileAndRunFile()
{
    QProcess process;
    QString Result;
    int result;
    result=process.execute(".\\compiler.exe \""+fileName+"\"");
    if(result!=0)
    {
        Result=QString::fromStdString("Compilation Error\nCode:"+std::to_string(result));
        QMessageBox::warning(this,tr("Compilation Error"),Result);
        return;
    }
    system(("start E:\\GitHub\\Logisim-Connection-Language\\IDE\\build-IDE-Desktop_Qt_5_12_9_MinGW_64_bit-Release\\release\\logisim-win-2.7.1.exe \""+(((fileName.toStdString()).substr(0,(fileName.toStdString()).find_last_of('.')))+".circ")).c_str());
}
void MainWindow::newFile()
{
    editor->clear();
    fileName.clear();
    setWindowTitle("LCL IDE - Untitled");
}

void MainWindow::openFile(const QString &path)
{
    fileName=path;
    if(fileName.isEmpty())
    {
        fileName=QFileDialog::getOpenFileName(this,tr("Open File"),"","LCL Files(*.lcl)");
    }
    if(!fileName.isEmpty())
    {
        QFile file(fileName);
        if(file.open(QFile::ReadOnly | QFile::Text))
        {
            editor->setPlainText(file.readAll());
        }
        setWindowTitle("LCL IDE - "+fileName);
    }
}
void MainWindow::saveFile()
{
    if(fileName.isEmpty())
    {
        fileName=QFileDialog::getSaveFileName(this,tr("Save File"),"","LCL Files(*.lcl)");
    }
    if(!fileName.isEmpty())
    {
        QFile file(fileName);
        if(file.open(QFile::WriteOnly | QFile::Text))
        {
            file.write(editor->document()->toPlainText().toStdString().c_str());
            file.close();
            setWindowTitle("LCL IDE - "+fileName);
        }
    }
}
void MainWindow::saveAs()
{
    QString fn;
    fn=QFileDialog::getSaveFileName(this,tr("Save As..."),"","LCL Files(*.lcl)");
    if(!fn.isEmpty())
    {
        QFile file(fn);
        if(file.open(QFile::WriteOnly | QFile::Text))
        {
            file.write(editor->document()->toPlainText().toStdString().c_str());
            file.close();
        }
    }
}
void MainWindow::setupGUI()
{
    fileName.clear();
    this->setStyleSheet("QWidget{background:#1e1e1e;color:#ffffff;}");
    menuBar()->setStyleSheet("QMenuBar{background:#323234;color:#ffffff;}");
    QFont font;
    font.setFamily("Consolas");
    font.setFixedPitch(true);
    font.setPointSize(13);

    editor=new QTextEdit;
    editor->setFont(font);
    menuBar()->setFont(font);

    highlighter=new Highlighter(editor->document());

    fileMenu=new QMenu(tr("&File"), this);
    menuBar()->addMenu(fileMenu);

    newAction=new QAction(tr("&New"),this);
    newAction->setShortcuts(QKeySequence::New);
    connect(newAction,SIGNAL(triggered()),this,SLOT(newFile()));
    fileMenu->addAction(newAction);

    openAction=new QAction(tr("&Open..."),this);
    openAction->setShortcuts(QKeySequence::Open);
    connect(openAction,SIGNAL(triggered()),this,SLOT(openFile()));
    fileMenu->addAction(openAction);

    saveAction=new QAction(tr("&Save"),this);
    saveAction->setShortcuts(QKeySequence::Save);
    connect(saveAction,SIGNAL(triggered()),this,SLOT(saveFile()));
    fileMenu->addAction(saveAction);

    saveAsAction=new QAction(tr("&Save As..."),this);
    saveAsAction->setShortcuts(QKeySequence::SaveAs);
    connect(saveAsAction,SIGNAL(triggered()),this,SLOT(saveAs()));
    fileMenu->addAction(saveAsAction);

    runMenu=new QMenu(tr("&Run"), this);
    menuBar()->addMenu(runMenu);

    compileAction=new QAction(tr("&Compile"),this);
    connect(compileAction,SIGNAL(triggered()),this,SLOT(compileFile()));
    runMenu->addAction(compileAction);
    compileAction=new QAction(tr("&Run"),this);
    connect(compileAction,SIGNAL(triggered()),this,SLOT(runFile()));
    runMenu->addAction(compileAction);
    compileAction=new QAction(tr("&Compile && Run"),this);
    connect(compileAction,SIGNAL(triggered()),this,SLOT(compileAndRunFile()));
    runMenu->addAction(compileAction);

    setCentralWidget(editor);
    setWindowTitle("LCL IDE - Untitled");
}
