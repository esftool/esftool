del.subset <- function(pts, poly){
  #Hyeongmo Koo (08/24/2017)
  #pts: SpatialPointsDataFrame object from readShapePoints:: input point dataset
  #poly: SpatialPolygonsDataFrame object from readShapePoly:: input study area (polygon)
  
  
  crds <- pts@coords
  z <- deldir(crds[,1], crds[,2])
  w <- tile.list(z)
  polys <- vector(mode='list', length=length(w))
  for (i in seq(along=polys)) {
    pcrds <- cbind(w[[i]]$x, w[[i]]$y)
    pcrds <- rbind(pcrds, pcrds[1,])
    polys[[i]] <- Polygons(list(Polygon(pcrds)), ID=as.character(i))
  }
  SP <- SpatialPolygons(polys)
  
  disPloy <- gUnaryUnion(poly)
  Clipped_polys <- gIntersection(disPloy, SP, byid = T)
  sample.nb <- poly2nb(Clipped_polys) ##Queen's definition.
  return(sample.nb)
}