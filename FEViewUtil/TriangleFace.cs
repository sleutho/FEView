using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEViewUtil
{
    class TriangleFace
    {
        CADTriangleFace(void) {}
    virtual ~CADTriangleFace(void) {}

    void Init(int i[3]);

    const CADPoint& GetModelCoord(const CADVertex* pVerts, int i) const;
    const CADPoint& GetViewCoord(const CADVertex* pVerts, int i) const;
    int             GetPixelX(const CADVertex* pVerts, int i) const;
    int             GetPixelY(const CADVertex* pVerts, int i) const;

    const CADTriangleFace* GetNext(void) const { return _pNext; }

private:
    int _i[3];

    const CADTriangleFace* _pNext;
    }
}
