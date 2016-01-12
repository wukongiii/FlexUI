using System;
using UnityEngine;
using CSLE;

namespace catwins.flexui
{
    class ScriptLogger : CSLE.ICLS_Logger
    {
        
        public void Log(string str)
        {
            Debug.Log(str);
        }
        
        public void Log_Error(string str)
        {
            Debug.LogError(str);
        }
        
        public void Log_Warn(string str)
        {
            Debug.LogWarning(str);
        }
    }
    
    public class CSEngine
    {
        public CSLE.CLS_Environment env = null;

        public CSEngine()
        {
            Init();
        }
        
        public void Init()
        {
            if (env == null)
            {
                env = new CSLE.CLS_Environment(new ScriptLogger());
            }
        }

        public void Reset()
        {
            env = null;
            Init();
        }
        
        public object Eval(string script)
        {
            if (env == null)
                Init();
            
            var token = env.ParserToken(script);//词法分析
            var expr = env.Expr_CompilerToken(token, true);//语法分析,简单表达式，一句话
            var value = env.Expr_Execute(expr, content);//执行表达式
            if (value == null) return null;
            return value.value;
        }
        public object Execute(string script)
        {
            var token = env.ParserToken(script);//词法分析
            var expr = env.Expr_CompilerToken(token, false);//语法分析，语法块
            var value = env.Expr_Execute(expr, content);//执行表达式
            if (value == null) return null;
            return value.value;
        }
        public void BuildFile(string filename, string code)
        {
            var token = env.ParserToken(code);//词法分析
            env.File_CompileToken(filename, token, false);
        }
        
        
        public CSLE.CLS_Content content = null;
        public void SetValue(string name, object v)
        {
            if (content == null)
                content = env.CreateContent();
            content.DefineAndSet(name, v.GetType(), v);
        }
        public void ClearValue()
        {
            content = null;
        }

        public void Dispose()
        {
            content = null;
            env = null;
        }
    }

}



