#include "mainwindow.h"

#include <QApplication>

int main(int argc,char *argv[])
{
    QApplication app(argc, argv);
    MainWindow window;
    if(argc!=1)
    {
        window.setFileName(argv[1]);
    }
    window.resize(640, 512);
    window.show();
    return app.exec();
}
