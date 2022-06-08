#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "highlighter.h"

#include <QMainWindow>

QT_BEGIN_NAMESPACE
class QTextEdit;
QT_END_NAMESPACE

class MainWindow:public QMainWindow
{
    Q_OBJECT
    public:
        MainWindow(QWidget *parent = 0);
        void setupGUI();
        QString fileName;
        QTextEdit *editor;
        Highlighter *highlighter;
        QMenu *fileMenu;
        QMenu *runMenu;
        QAction *newAction;
        QAction *openAction;
        QAction *saveAction;
        QAction *saveAsAction;
        QAction *compileAction;
    public slots:
        void newFile();
        void openFile(const QString &path = QString());
        void saveFile();
        void saveAs();
        void compileFile();
        void runFile();
        void compileAndRunFile();
};
#endif
