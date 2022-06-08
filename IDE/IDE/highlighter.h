#ifndef HIGHLIGHTER_H
#define HIGHLIGHTER_H

#include <QSyntaxHighlighter>
#include <QTextCharFormat>
#include <QRegularExpression>

QT_BEGIN_NAMESPACE
class QTextDocument;
QT_END_NAMESPACE

class Highlighter:public QSyntaxHighlighter
{
    Q_OBJECT
    public:
        Highlighter(QTextDocument *parent = 0);

        QTextCharFormat keywordFormat;
        QRegularExpression keywordPattern[8];
        QTextCharFormat keywordErrorFormat;
        QRegularExpression keywordErrorPattern[8];
        QTextCharFormat commentFormat;
        QRegularExpression commentPattern;
        QTextCharFormat numberFormat;
        QRegularExpression numberPattern;
        QTextCharFormat builtInVarFormat;
        QRegularExpression builtInVarPattern;
        QTextCharFormat symbolFormat;
        QRegularExpression symbolPattern;
    protected:
        void highlightBlock(const QString &text) override;
    private:
        void setTextFormat();
        void setTextPattern();
};
#endif
