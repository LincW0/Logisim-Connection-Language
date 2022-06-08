#include "highlighter.h"

Highlighter::Highlighter(QTextDocument *parent):QSyntaxHighlighter(parent)
{
    setTextFormat();
    setTextPattern();
}
void Highlighter::setTextFormat()
{
    keywordFormat.setForeground(QColor(88,144,255));
    keywordErrorFormat.setForeground(Qt::red);
    commentFormat.setForeground(QColor(115,142,111));
    numberFormat.setForeground(QColor(183,226,154));
    builtInVarFormat.setForeground(QColor(158,233,255));
    symbolFormat.setForeground(QColor(88,144,255));
}
void Highlighter::setTextPattern()
{
    keywordPattern[0]=QRegularExpression("^\\bIN\\b");
    keywordPattern[1]=QRegularExpression("^\\bOUT\\b");
    keywordPattern[2]=QRegularExpression("^\\bAND\\b");
    keywordPattern[3]=QRegularExpression("^\\bXOR\\b");
    keywordPattern[4]=QRegularExpression("^\\bOR\\b");
    keywordPattern[5]=QRegularExpression("^\\bNOT\\b");
    keywordPattern[6]=QRegularExpression("^\\bCNCT\\b");

    keywordErrorPattern[0]=QRegularExpression("/?(\\bIN\\b.*|\\bOUT\\b.*|\\bAND\\b.*|\\bXOR\\b.*|\\bOR\\b.*|\\bNOT\\b.*|\\bCNCT\\b.*)\\bIN\\b");
    keywordErrorPattern[1]=QRegularExpression("/?(\\bIN\\b.*|\\bOUT\\b.*|\\bAND\\b.*|\\bXOR\\b.*|\\bOR\\b.*|\\bNOT\\b.*|\\bCNCT\\b.*)\\bOUT\\b");
    keywordErrorPattern[2]=QRegularExpression("/?(\\bIN\\b.*|\\bOUT\\b.*|\\bAND\\b.*|\\bXOR\\b.*|\\bOR\\b.*|\\bNOT\\b.*|\\bCNCT\\b.*)\\bAND\\b");
    keywordErrorPattern[3]=QRegularExpression("/?(\\bIN\\b.*|\\bOUT\\b.*|\\bAND\\b.*|\\bXOR\\b.*|\\bOR\\b.*|\\bNOT\\b.*|\\bCNCT\\b.*)\\bXOR\\b");
    keywordErrorPattern[4]=QRegularExpression("/?(\\bIN\\b.*|\\bOUT\\b.*|\\bAND\\b.*|\\bXOR\\b.*|\\bOR\\b.*|\\bNOT\\b.*|\\bCNCT\\b.*)\\bOR\\b");
    keywordErrorPattern[5]=QRegularExpression("/?(\\bIN\\b.*|\\bOUT\\b.*|\\bAND\\b.*|\\bXOR\\b.*|\\bOR\\b.*|\\bNOT\\b.*|\\bCNCT\\b.*)\\bNOT\\b");
    keywordErrorPattern[6]=QRegularExpression("/?(\\bIN\\b.*|\\bOUT\\b.*|\\bAND\\b.*|\\bXOR\\b.*|\\bOR\\b.*|\\bNOT\\b.*|\\bCNCT\\b.*)\\bCNCT\\b");

    commentPattern=QRegularExpression("//[^\n]*");
    numberPattern=QRegularExpression("[0-9]");
    builtInVarPattern=QRegularExpression("\\binput\\b|\\boutput\\b|\\band\\b|\\bxor\\b|\\bor\\b|\\bnot\\b");
    symbolPattern=QRegularExpression("\\[|\\]");
}
void Highlighter::highlightBlock(const QString &text)
{
    QRegularExpressionMatchIterator matchIterator;
    QRegularExpressionMatch match;
    matchIterator=numberPattern.globalMatch(text);
    while (matchIterator.hasNext())
    {
        QRegularExpressionMatch match=matchIterator.next();
        setFormat(match.capturedStart(),match.capturedLength(),numberFormat);
    }
    matchIterator=builtInVarPattern.globalMatch(text);
    while (matchIterator.hasNext())
    {
        QRegularExpressionMatch match=matchIterator.next();
        setFormat(match.capturedStart(),match.capturedLength(),builtInVarFormat);
    }
    matchIterator=symbolPattern.globalMatch(text);
    while (matchIterator.hasNext())
    {
        QRegularExpressionMatch match=matchIterator.next();
        setFormat(match.capturedStart(),match.capturedLength(),symbolFormat);
    }
    for(int i=0;i<7;++i)
    {
        matchIterator=keywordPattern[i].globalMatch(text);
        while (matchIterator.hasNext())
        {
            match=matchIterator.next();
            setFormat(match.capturedStart(),match.capturedLength(),keywordFormat);
        }
        matchIterator=keywordErrorPattern[i].globalMatch(text);
        while (matchIterator.hasNext())
        {
            match=matchIterator.next();
            setFormat(match.capturedStart(),match.capturedLength(),keywordErrorFormat);
        }
    }
    matchIterator=commentPattern.globalMatch(text);
    while (matchIterator.hasNext())
    {
        QRegularExpressionMatch match=matchIterator.next();
        setFormat(match.capturedStart(),match.capturedLength(),commentFormat);
    }
    setCurrentBlockState(0);
}
