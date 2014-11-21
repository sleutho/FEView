using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEViewUtil
{
    class Model
    {
        CADModel(void);
    virtual ~CADModel(void);

    void Reset(void);
    bool New(const wchar_t* szName); // *.FEM-file
    void FinalizeViewTransformation(void);

    const wchar_t*   GetName(void)              { return _pszName; }
    const wchar_t*   GetTitle(void)             { return _pszTitle; }
    const CADBox&    GetModelBox(void) const    { return _modelBox; }
    const CADBox&    GetViewBox(void) const     { return _viewBox; }

    int              GetNoVerts(void) const     { return _nVerts; }
    CADVertex*       GetVertex(int i);

    const CADVertex* GetVertexArray(void) const { return _pVerts; }
    CADTriangleFace* GetFirstFace(void)         { return _pFirstFace; }


private:
    wchar_t*         _pszName;
    wchar_t*         _pszTitle;

    int              _nVerts;
    CADVertex*       _pVerts;

    int              _nFaces;
    CADTriangleFace* _pFaces;
    CADTriangleFace* _pFirstFace;

    CADBox           _modelBox;
    CADBox           _viewBox;
    }
}
